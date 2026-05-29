// Ollama (local LLM) implementation
import { LLM } from './index';

export class OllamaLLM implements LLM {
  private endpoint: string;

  constructor(config: { endpoint?: string }) {
    this.endpoint = config.endpoint || 'http://localhost:11434';
  }

  async generateResponse(prompt: string): Promise<string> {
    // TODO: Implement Ollama API call
    return 'Ollama LLM response (stub)';
  }
}
