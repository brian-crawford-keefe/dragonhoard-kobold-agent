// Unified interface for LLM calls
export interface LLM {
  generateResponse(prompt: string): Promise<string>;
}

export async function getLLM(type: 'remote' | 'ollama', config: any): Promise<LLM> {
  if (type === 'ollama') {
    const { OllamaLLM } = await import('./ollamaLLM');
    return new OllamaLLM(config);
  } else {
    const { RemoteLLM } = await import('./remoteLLM');
    return new RemoteLLM(config);
  }
}
