// Remote LLM (OpenAI/Azure) implementation
import { LLM } from './index';

export class RemoteLLM implements LLM {
  private apiKey: string;
  private endpoint: string;

  constructor(config: { apiKey: string; endpoint: string }) {
    this.apiKey = config.apiKey;
    this.endpoint = config.endpoint;
  }

  async generateResponse(prompt: string): Promise<string> {
    // TODO: Implement remote LLM call (OpenAI/Azure)
    return 'Remote LLM response (stub)';
  }
}
