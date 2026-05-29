using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Web.WebView2.Core;

namespace DragonHoard.VSIX
{
    [Guid("c2e2c2e0-3456-4cde-9abc-3456789abcde")]
    public class SummaryToolWindow : ToolWindowPane
    {
        private TextBox _textBox;
        private WebBrowser _browser;
        private ToggleButton _renderToggle;
        private Button _copyBtn;
        private Button _saveBtn;
        private Button _clearBtn;
        private Button _cancelBtn;
        private ProgressBar _progressBar;

        public SummaryToolWindow() : base(null)
        {
            this.Caption = "Kobold Clan Summary";

            var dock = new DockPanel();

            var toolbar = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(4) };
            _copyBtn = new Button { Content = "Copy", Margin = new Thickness(2) };
            _copyBtn.Click += (s, e) => CopyToClipboard();
            _saveBtn = new Button { Content = "Save...", Margin = new Thickness(2) };
            _saveBtn.Click += (s, e) => SaveToFile();
            _clearBtn = new Button { Content = "Clear", Margin = new Thickness(2) };
            _clearBtn.Click += (s, e) => SetText(string.Empty);
            _renderToggle = new ToggleButton { Content = "Render Markdown", Margin = new Thickness(8,2,2,2) };
            _renderToggle.Checked += (s, e) => UpdateRendered();
            _renderToggle.Unchecked += (s, e) => UpdateRendered();

            toolbar.Children.Add(_copyBtn);
            toolbar.Children.Add(_saveBtn);
            toolbar.Children.Add(_clearBtn);
            _cancelBtn = new Button { Content = "Cancel", Margin = new Thickness(6), IsEnabled = false };
            toolbar.Children.Add(_cancelBtn);
            toolbar.Children.Add(_renderToggle);

            DockPanel.SetDock(toolbar, Dock.Top);
            dock.Children.Add(toolbar);

            _progressBar = new ProgressBar { IsIndeterminate = true, Visibility = Visibility.Collapsed, Height = 6, Margin = new Thickness(4) };
            DockPanel.SetDock(_progressBar, Dock.Top);
            dock.Children.Add(_progressBar);

            _cancelBtn.Click += (s, e) => _cancelCallback?.Invoke();

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            _textBox = new TextBox
            {
                AcceptsReturn = true,
                AcceptsTab = true,
                TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                IsReadOnly = true
            };
            Grid.SetRow(_textBox, 0);

            _browser = new WebBrowser();
            WebView2 _webView = null;
            try
            {
                _webView = new WebView2();
                Grid.SetRow(_webView, 1);
            }
            catch
            {
                _webView = null;
            }
            Grid.SetRow(_browser, 1);

            grid.Children.Add(_textBox);
            if (_webView != null)
            {
                grid.Children.Add(_webView);
            }
            else
            {
                grid.Children.Add(_browser);
            }

            dock.Children.Add(grid);

            this.Content = dock;
        }

        private Action _cancelCallback;

        private void CopyToClipboard()
        {
            var text = _textBox?.Text ?? string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }

        public void SetBusy(bool busy)
        {
            if (this.Dispatcher.CheckAccess())
            {
                _copyBtn.IsEnabled = !busy;
                _saveBtn.IsEnabled = !busy;
                _clearBtn.IsEnabled = !busy;
                _renderToggle.IsEnabled = !busy;
                _progressBar.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
                _cancelBtn.IsEnabled = busy;
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SetBusy(busy)));
            }
        }

        public void SetCancelCallback(Action callback)
        {
            _cancelCallback = callback;
        }

        public void ClearCancelCallback()
        {
            _cancelCallback = null;
        }
        }

        private void SaveToFile()
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Text Files|*.txt|Markdown|*.md|All Files|*.*",
                FileName = "kobold-summary.md"
            };
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, _textBox.Text ?? string.Empty);
            }
        }

        private async void UpdateRendered()
        {
            var html = MarkdownToHtml(_textBox.Text ?? string.Empty);
            // prefer WebView2 if available
            try
            {
                var webview = FindWebView();
                if (webview != null)
                {
                    if (webview.CoreWebView2 == null)
                    {
                        await webview.EnsureCoreWebView2Async();
                    }
                    if (_renderToggle.IsChecked == true)
                    {
                        webview.Visibility = Visibility.Visible;
                        _textBox.Visibility = Visibility.Collapsed;
                        webview.CoreWebView2.NavigateToString(html);
                    }
                    else
                    {
                        webview.Visibility = Visibility.Collapsed;
                        _textBox.Visibility = Visibility.Visible;
                    }
                    return;
                }
            }
            catch
            {
                // ignore and fallback to WebBrowser/TextBox
            }

            if (_renderToggle.IsChecked == true)
            {
                _browser.NavigateToString(html);
                _browser.Visibility = Visibility.Visible;
                _textBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                _browser.Visibility = Visibility.Collapsed;
                _textBox.Visibility = Visibility.Visible;
            }
        }

        private WebView2 FindWebView()
        {
            foreach (var child in ((Grid)((DockPanel)this.Content).Children[1]).Children)
            {
                if (child is WebView2 wv) return wv;
            }
            return null;
        }

        private string MarkdownToHtml(string md)
        {
            if (string.IsNullOrEmpty(md)) return "<html><body></body></html>";
            // very small markdown -> HTML transformations for basic readability
            var html = System.Net.WebUtility.HtmlEncode(md);
            // headers
            html = System.Text.RegularExpressions.Regex.Replace(html, "^###### (.*)$", "<h6>$1</h6>", System.Text.RegularExpressions.RegexOptions.Multiline);
            html = System.Text.RegularExpressions.Regex.Replace(html, "^##### (.*)$", "<h5>$1</h5>", System.Text.RegularExpressions.RegexOptions.Multiline);
            html = System.Text.RegularExpressions.Regex.Replace(html, "^#### (.*)$", "<h4>$1</h4>", System.Text.RegularExpressions.RegexOptions.Multiline);
            html = System.Text.RegularExpressions.Regex.Replace(html, "^### (.*)$", "<h3>$1</h3>", System.Text.RegularExpressions.RegexOptions.Multiline);
            html = System.Text.RegularExpressions.Regex.Replace(html, "^## (.*)$", "<h2>$1</h2>", System.Text.RegularExpressions.RegexOptions.Multiline);
            html = System.Text.RegularExpressions.Regex.Replace(html, "^# (.*)$", "<h1>$1</h1>", System.Text.RegularExpressions.RegexOptions.Multiline);
            // bold **text**
            html = System.Text.RegularExpressions.Regex.Replace(html, "\\*\\*(.+?)\\*\\*", "<strong>$1</strong>");
            // italics *text*
            html = System.Text.RegularExpressions.Regex.Replace(html, "\\*(.+?)\\*", "<em>$1</em>");
            // links [text](url)
            html = System.Text.RegularExpressions.Regex.Replace(html, "\\[(.+?)\\]\\((.+?)\\)", "<a href=\"$2\">$1</a>");
            // code fences ```
            html = System.Text.RegularExpressions.Regex.Replace(html, "```(?:\\w+)?\\n([\\s\\S]*?)```", "<pre><code>$1</code></pre>", System.Text.RegularExpressions.RegexOptions.Multiline);
            // inline code `code`
            html = System.Text.RegularExpressions.Regex.Replace(html, "`([^`]+)`", "<code>$1</code>");

            // paragraphs
            html = "<html><head><meta charset=\"utf-8\"/><style>body{font-family:Segoe UI,Calibri,Arial; padding:8px;} pre{background:#f3f3f3;padding:8px;border-radius:4px;} code{background:#eee;padding:2px 4px;border-radius:3px;} a{color:#2b7;}</style></head><body>" + html.Replace("\n\n", "</p><p>") + "</body></html>";
            return html;
        }

        public void SetText(string text)
        {
            // Ensure running on UI thread
            if (this.Dispatcher.CheckAccess())
            {
                _textBox.Text = text ?? string.Empty;
                if (_renderToggle.IsChecked == true)
                    UpdateRendered();
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SetText(text)));
            }
        }

        public void SetBusy(bool busy)
        {
            if (this.Dispatcher.CheckAccess())
            {
                _copyBtn.IsEnabled = !busy;
                _saveBtn.IsEnabled = !busy;
                _clearBtn.IsEnabled = !busy;
                _renderToggle.IsEnabled = !busy;
                _progressBar.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SetBusy(busy)));
            }
        }
    }
}
