
import * as vscode from 'vscode';
import { KoboldEngine } from './koboldEngine';
import { getExtensionSettings } from './config/settings';
import * as path from 'path';
import * as fs from 'fs';

let koboldEngine: KoboldEngine;


export async function activate(context: vscode.ExtensionContext) {
  // Load settings from VS Code configuration
  const settings = getExtensionSettings();
  koboldEngine = new KoboldEngine(settings);
  await koboldEngine.initialize();

  // Command to reload kobold settings
  const reloadDisposable = vscode.commands.registerCommand('koboldClan.reloadSettings', async () => {
    const settings = getExtensionSettings();
    koboldEngine = new KoboldEngine(settings);
    await koboldEngine.initialize();
    vscode.window.showInformationMessage('Kobold Clan settings reloaded!');
  });

  let disposable = vscode.commands.registerCommand('koboldClan.askKobolds', async () => {
    const userPrompt = await vscode.window.showInputBox({ prompt: 'What is your command for the kobolds, Dragon?' });
    if (!userPrompt) return;
    const response = await koboldEngine.askKobolds(userPrompt);
    vscode.window.showInformationMessage(response);
  });

  // Command: Summon Clan Summary - captures selection or whole file and posts to KoboldBrain
  const summaryDisposable = vscode.commands.registerCommand('koboldClan.summonClanSummary', async () => {
    const editor = vscode.window.activeTextEditor;
    if (!editor) {
      vscode.window.showInformationMessage('Dragon, open a file to summarize.');
      return;
    }

    const selection = editor.selection;
    const code = selection && !selection.isEmpty ? editor.document.getText(selection) : editor.document.getText();
    if (!code || code.trim().length === 0) {
      vscode.window.showInformationMessage('Dragon, I found no selection or document text to summarize.');
      return;
    }

    const panel = vscode.window.createWebviewPanel('koboldClanSummary', 'Kobold Clan Summary', vscode.ViewColumn.Beside, {
      enableScripts: true,
      retainContextWhenHidden: true
    });

    panel.webview.html = getWebviewContent();

    const controller = new AbortController();

    // Listen for messages from the webview (e.g., cancel)
    const messageDisposable = panel.webview.onDidReceiveMessage(msg => {
      if (msg?.command === 'cancel') {
        try { controller.abort(); } catch { }
      }
    });

    panel.onDidDispose(() => {
      try { controller.abort(); } catch { }
      messageDisposable.dispose();
    });

    // Tell webview we're starting
    panel.webview.postMessage({ command: 'status', status: 'starting' });

    try {
      const resp = await fetch('http://localhost:5077/summarize', {
        method: 'POST',
        headers: { 'Content-Type': 'text/plain; charset=utf-8' },
        body: code,
        signal: controller.signal
      });
      if (!resp.ok) {
        const text = await resp.text();
        panel.webview.postMessage({ command: 'error', error: `KoboldBrain error: ${resp.status} ${text}` });
        return;
      }
      const json = await resp.json();
      const summary = json?.summary ?? '(no summary)';
      panel.webview.postMessage({ command: 'show', summary });
    }
    catch (e: any) {
      if (e?.name === 'AbortError') {
        panel.webview.postMessage({ command: 'canceled' });
      } else {
        panel.webview.postMessage({ command: 'error', error: String(e?.message ?? e) });
      }
    }
  });

  context.subscriptions.push(summaryDisposable);

  function getWebviewContent(): string {
    try {
      const extensionPath = context.extensionPath;
      const filePath = path.join(extensionPath, 'src', 'webview', 'summarizer.html');
      return fs.readFileSync(filePath, { encoding: 'utf8' });
    } catch (e) {
      return `<html><body><pre>Failed to load webview content: ${String(e)}</pre></body></html>`;
    }
  }

  context.subscriptions.push(disposable);
  context.subscriptions.push(reloadDisposable);
}

export function deactivate() {}
