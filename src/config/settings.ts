import * as vscode from 'vscode';

export type LLMType = 'ollama' | 'remote';


export interface KoboldSettings {
  llmType: LLMType;
  ollamaEndpoint: string;
  remoteApiKey: string;
  remoteEndpoint: string;
  koboldFlavor: boolean;
  activeKobolds: string[];
  customQuirks: Record<string, string>;
  enableTestCoverage: boolean;
  enableSecurityScan: boolean;
  enableRefactorHelper: boolean;
  enableQuickFixes: boolean;
}

export const defaultSettings: KoboldSettings = {
  llmType: 'ollama',
  ollamaEndpoint: 'http://localhost:11434',
  remoteApiKey: '',
  remoteEndpoint: '',
  koboldFlavor: true,
  activeKobolds: ['Kib', 'Meepo', 'Drak', 'Snip', 'Vex', 'Pip', 'Nubbin'],
  customQuirks: {},
  enableTestCoverage: false,
  enableSecurityScan: false,
  enableRefactorHelper: false,
  enableQuickFixes: false,
};

export function getExtensionSettings(): KoboldSettings {
  const config = vscode.workspace.getConfiguration('koboldClan');
  return {
    llmType: config.get<LLMType>('llmType', defaultSettings.llmType),
    ollamaEndpoint: config.get<string>('ollamaEndpoint', defaultSettings.ollamaEndpoint),
    remoteApiKey: config.get<string>('remoteApiKey', defaultSettings.remoteApiKey),
    remoteEndpoint: config.get<string>('remoteEndpoint', defaultSettings.remoteEndpoint),
    koboldFlavor: config.get<boolean>('koboldFlavor', defaultSettings.koboldFlavor),
    activeKobolds: config.get<string[]>('activeKobolds', defaultSettings.activeKobolds),
    customQuirks: config.get<Record<string, string>>('customQuirks', defaultSettings.customQuirks),
    enableTestCoverage: config.get<boolean>('enableTestCoverage', defaultSettings.enableTestCoverage),
    enableSecurityScan: config.get<boolean>('enableSecurityScan', defaultSettings.enableSecurityScan),
    enableRefactorHelper: config.get<boolean>('enableRefactorHelper', defaultSettings.enableRefactorHelper),
    enableQuickFixes: config.get<boolean>('enableQuickFixes', defaultSettings.enableQuickFixes),
  };
}
