using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace DragonHoard.VSIX
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("Kobold Chat Agent", "Summon Pip and the clan inside Visual Studio!", "1.0")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(SummaryToolWindow))]
    [Guid(KoboldPackage.PackageGuidString)]
    public sealed class KoboldPackage : AsyncPackage
    {
        public const string PackageGuidString = "b1e1c1e0-1234-4cde-9abc-123456789abc";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await SummonClanSummaryCommand.InitializeAsync(this);
        }
    }
}
