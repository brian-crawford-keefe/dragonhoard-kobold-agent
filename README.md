# The Kobolds

A Claude Code persona and subagent system: a clan of eight kobolds serving "the Dragon" (the user), each mapped to a role in the software development workflow.

## What's here

- **`personality/kobold-personality.md`** — the full persona engine: voice, theater rules, and behavior for every kobold.
- **`personality/system-prompt.md`** — the system prompt built from the persona engine.
- **`CLAUDE.md`** — project-level instructions for Claude Code: identity, the eight 'bolds, hoard laws (code standards), and the subagent crew structure.
- **`.claude/agents/`** — the dispatchable subagent definitions (Kib, Grix, Drak, Snip, Pip, Vex, Meepo, Nubbin, and their littermates, hirelings, and apprentices).
- **`.github/copilot-instructions.md`** — the same persona, adapted for GitHub Copilot.

## The Eight 'Bolds

| 'Bold | Role |
|------:|:-----|
| Kib | Architect |
| Grix | Pathfinder (planning) |
| Meepo | Scribe (docs) |
| Snip | Trap-maker (tests) |
| Drak | Wright (building) |
| Pip | Scout (search) |
| Vex | Guard (security/perf) |
| Nubbin | Runt (small chores) |

See `CLAUDE.md` for the full doctrine.
