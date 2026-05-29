using System;
using System.Threading;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace DragonHoard.VSIX
{
    internal sealed class SummonClanSummaryCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("d1e1c1e0-1234-4cde-9abc-123456789abc");
        private readonly AsyncPackage package;
        private int _isRunning = 0;
        private OleMenuCommand _menuCommand;

        private SummonClanSummaryCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            var menuCommandID = new CommandID(CommandSet, CommandId);
            this._menuCommand = new OleMenuCommand(this.Execute, menuCommandID);
            this._menuCommand.BeforeQueryStatus += OnBeforeQueryStatus;
            commandService.AddCommand(this._menuCommand);
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            new SummonClanSummaryCommand(package, commandService);
        }

        private void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            try
            {
                if (_menuCommand == null) return;
                _menuCommand.Enabled = Interlocked.CompareExchange(ref _isRunning, 0, 0) == 0;
            }
            catch
            {
                // ignore
            }
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Concurrency guard: only one summary at a time
            if (Interlocked.Exchange(ref _isRunning, 1) == 1)
            {
                System.Windows.MessageBox.Show("Dragon, a summary is already running. Please wait or cancel the current one.", "Kobold Clan: Summon Clan Summary");
                return;
            }

            // disable the command in the UI so the Dragon can't start another
            try
            {
                _menuCommand.Enabled = false;
            }
            catch
            {
                // ignore if UI thread constraints or menu not available
            }

            // Run async work without blocking the UI thread
            _ = this.package.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    var code = await EditorUtils.GetSelectedOrAllTextAsync(this.package).ConfigureAwait(false);
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                    if (string.IsNullOrWhiteSpace(code))
                    {
                        System.Windows.MessageBox.Show("Dragon, I found no selection or document text to summarize.", "Kobold Clan: Summon Clan Summary");
                        return;
                    }

                    string summary;
                    var cts = new System.Threading.CancellationTokenSource();
                    var window = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                    if (window is SummaryToolWindow stwStart)
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        stwStart.SetBusy(true);
                        stwStart.SetCancelCallback(() => cts.Cancel());
                    }

                    try
                    {
                        summary = await KoboldBrainClient.SummarizeAsync(code, cts.Token).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        var wnd = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                        if (wnd is SummaryToolWindow stwC)
                        {
                            stwC.SetBusy(false);
                            stwC.ClearCancelCallback();
                            stwC.SetText("Summary canceled by Dragon.");
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        System.Windows.MessageBox.Show($"Failed to reach KoboldBrain: {ex.Message}", "Kobold Clan: Error");
                        var wnd = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                        if (wnd is SummaryToolWindow stwErr)
                        {
                            stwErr.SetBusy(false);
                            stwErr.ClearCancelCallback();
                        }
                        return;
                    }

                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    var window2 = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                    if (window2?.Frame is null)
                    {
                        System.Windows.MessageBox.Show(summary ?? "(no summary)", "Kobold Clan Summary");
                    }
                    else
                    {
                        if (window2 is SummaryToolWindow stw)
                        {
                            stw.SetBusy(false);
                            stw.ClearCancelCallback();
                            stw.SetText(summary ?? "(no summary)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    var window = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                    if (window is SummaryToolWindow stwErr)
                    {
                        stwErr.SetBusy(false);
                        stwErr.ClearCancelCallback();
                    }
                    System.Windows.MessageBox.Show($"Unexpected error: {ex.Message}", "Kobold Clan: Error");
                }
                finally
                {
                    // ensure we always clear busy and running flag
                    try
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        var window = await this.package.ShowToolWindowAsync(typeof(SummaryToolWindow), 0, true, this.package.DisposalToken);
                        if (window is SummaryToolWindow stwFinal)
                        {
                            stwFinal.SetBusy(false);
                            stwFinal.ClearCancelCallback();
                        }
                    }
                    catch
                    {
                        // swallow; we're in cleanup
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isRunning, 0);
                        try
                        {
                            _menuCommand.Enabled = true;
                        }
                        catch
                        {
                            // ignore
                        }
                    }
                }
            });
        }
    }
}
