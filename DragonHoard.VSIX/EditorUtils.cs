using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Threading.Tasks;

namespace DragonHoard.VSIX
{
    internal static class EditorUtils
    {
        public static async Task<string> GetSelectedOrAllTextAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            if (dte?.ActiveDocument?.Object("TextDocument") is TextDocument textDoc)
            {
                var sel = textDoc.Selection;
                var text = sel?.Text;
                if (!string.IsNullOrEmpty(text))
                    return text;
                // If nothing selected, get all
                var start = textDoc.StartPoint.CreateEditPoint();
                return start.GetText(textDoc.EndPoint);
            }
            return string.Empty;
        }
    }
}
