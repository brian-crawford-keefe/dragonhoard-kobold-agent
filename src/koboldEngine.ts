
import { getLLM } from './llmIntegration/index';
import { KoboldSettings, defaultSettings } from './config/settings';
import * as fs from 'fs';
import * as path from 'path';

export class KoboldEngine {
  private settings: KoboldSettings;
  private persona: string;
  private systemPrompt: string;
  private llm: any;

  constructor(settings: KoboldSettings = defaultSettings) {
    this.settings = settings;
    this.persona = '';
    this.systemPrompt = '';
  }

  async initialize() {
    // Load persona and system prompt
    this.persona = fs.readFileSync(path.join(__dirname, 'personality', 'kobold-personality.md'), 'utf-8');
    this.systemPrompt = fs.readFileSync(path.join(__dirname, 'personality', 'system-prompt.md'), 'utf-8');
    // Load LLM
    this.llm = await getLLM(this.settings.llmType, this.settings);
  }

  // Map keywords to kobold roles
  private koboldRoleMap: { [role: string]: string[] } = {
    Kib: ['structure', 'architecture', 'project layout', 'split', 'merge', 'design'],
    Meepo: ['doc', 'documentation', 'comment', 'readme', 'explain', 'describe'],
    Drak: ['refactor', 'cleanup', 'remove', 'dead code', 'clutter', 'legacy'],
    Snip: ['test', 'unit test', 'mock', 'fixture', 'trap', 'coverage'],
    Vex: ['security', 'perf', 'performance', 'async', 'dependency', 'vulnerability', 'secret'],
    Pip: ['find', 'search', 'where', 'reference', 'call site', 'related'],
    Nubbin: ['rename', 'typo', 'small', 'tiny', 'quick', 'one-line', 'simple'],
  };

  // Extract persona section for a kobold
  // Static method to check for missing onboarding docs
  static async checkOnboardingDocs(): Promise<string[]> {
    // Use dynamic import to avoid issues in non-extension environments
    const vscode = await import('vscode');
    const workspaceFolders = vscode.workspace.workspaceFolders;
    if (!workspaceFolders) return [];
    const folderUri = workspaceFolders[0].uri;
    const files = await vscode.workspace.fs.readDirectory(folderUri);
    const fileNames = files.map(([name]: [string, any]) => name.toLowerCase());
    const missing: string[] = [];
    if (!fileNames.includes('readme.md')) missing.push('README.md');
    if (!fileNames.includes('architecture.md')) missing.push('ARCHITECTURE.md');
    if (!fileNames.includes('overview.md')) missing.push('OVERVIEW.md');
    return missing;
  }
  private getKoboldPersona(role: string): string {
    const match = this.persona.match(new RegExp(`### ${role}(.|\n)*?(?=###|---|$)`, 'g'));
    return match ? match[0].trim() : '';
  }

  // Determine which kobold should answer, considering only active kobolds and improved context
  private routeKobold(userPrompt: string): string {
    const promptLower = userPrompt.toLowerCase();
    // Only consider active kobolds from settings
    const activeRoles = this.settings.activeKobolds || Object.keys(this.koboldRoleMap);
    let candidates: string[] = [];
    for (const role of activeRoles) {
      const keywords = this.koboldRoleMap[role] || [];
      if (keywords.some(k => promptLower.includes(k))) {
        candidates.push(role);
      }
    }
    // If multiple kobolds match, prefer the one with the most keyword hits
    if (candidates.length > 1) {
      let maxHits = 0;
      let bestRole = candidates[0];
      for (const role of candidates) {
        const keywords = this.koboldRoleMap[role] || [];
        const hits = keywords.filter(k => promptLower.includes(k)).length;
        if (hits > maxHits) {
          maxHits = hits;
          bestRole = role;
        }
      }
      return bestRole;
    }
    if (candidates.length === 1) return candidates[0];
    return 'Sprocket'; // Default to Sprocket if no match
  }

  async askKobolds(userPrompt: string): Promise<string> {
    // Onboarding: Check for missing docs
    const missingDocs = await KoboldEngine.checkOnboardingDocs();
    let onboardingMsg = '';
    if (missingDocs.length > 0) {
      onboardingMsg = `*Meepo flourishes his quill.* Dragon, I sniffed out missing documentation: ${missingDocs.join(', ')}. Would you like me to scribe one?`;
    }

    const kobold = this.routeKobold(userPrompt);
    let personaSection = '';
    if (kobold === 'Sprocket') {
      // Sprocket mediates, include all personas for context
      personaSection = this.persona;
    } else {
      personaSection = this.getKoboldPersona(kobold);
      // Add Sprocket's intro for flavor
      personaSection = this.getKoboldPersona('Sprocket') + '\n\n' + personaSection;
    }
    const fullPrompt = `${this.systemPrompt}\n\n${personaSection}\n\n${userPrompt}`;
    const response = await this.llm.generateResponse(fullPrompt);
    return onboardingMsg ? `${onboardingMsg}\n\n${response}` : response;
  }
}
