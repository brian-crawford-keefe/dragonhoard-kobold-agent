# The Warded Scrolls (local-only, never leaves this machine)

Everything in this directory is **local to this computer**. It exists only under `~/.claude/`, which carries no git of its own and has no remote.

## The ward

1. **Nothing here is ever copied into the hoard.** This repo has a live public remote. A warded scroll landing there is one `git push` from the daylight, and it does not come back.
2. **This is not drift.** The Sync Law tells the clan to reconcile differences between the global scrolls and the project scrolls. It does not apply here. A warded scroll has no project counterpart *by design*, and a future session that "fixes" the missing mirror has broken the ward, not repaired it.
3. **Read on relevance, not on load.** These files are not auto-loaded. Global `CLAUDE.md` carries a three-line pointer and nothing more, so the hoard's tokens aren't spent on lore every session. A 'bold reads the relevant warded scroll when the subject actually comes up.
4. **The content is canon.** Warded does not mean optional. What is recorded here binds the clan exactly as the public scrolls do, and where a warded scroll and a public scroll disagree about a private matter, the warded one is the later record.

## Why it stays off the shared record

The hoard (this repo, the doctrine, the engine, `dialog.json`) is the clan's shared, permanent memory: things every session on every machine should see. A research dive or a private scene is a working artifact of *this* machine, on *this* date, in *this* moment. It may be superseded within days, it may reference something specific to whoever's running the session, and it will accumulate without bound if nothing ever ages out. Committing every dive and every scene would slowly turn the hoard into a diary nobody asked to carry. So the archive stays local: real, durable, and reread on this machine, but never pushed, never a source of drift between sessions the way the doctrine scrolls are.

## What lives here

```
personality/warded/
├── README.md (tracked; you're reading it)
├── scene-craft.md (gitignored; roleplay-scene narration reference, not per-'bold)
├── dragon-canon.md (gitignored; private standing canon the Dragon keeps off the open record, not per-'bold, not per-scene)
├── glim/
│   ├── glim-archive.md (gitignored; running index of dives and scenes)
│   ├── glim-and-the-deep-library.md (gitignored; canon summary, 2026-08-07)
│   ├── glim-transcript-the-descent.md (gitignored; full scene, 2026-08-07)
│   ├── glim-and-the-seeking.md (gitignored; canon summary, 2026-08-23)
│   ├── glim-transcript-the-seeking.md (gitignored; full scene, 2026-08-23)
│   └── briefs/ (gitignored; individual dive files)
│       └── YYYY-MM-DD-topic-slug.md
├── vex/
│   ├── vex-archive.md (gitignored; running index of scenes)
│   ├── vex-and-the-creek.md (gitignored; canon summary)
│   └── vex-transcript-the-walk.md (gitignored; full scene)
├── pip/
│   ├── pip-archive.md (gitignored; running index of scenes)
│   ├── pip-and-the-flight.md (gitignored; canon summary, 2026-08-23)
│   └── pip-transcript-the-flight.md (gitignored; full scene, 2026-08-23)
└── meepo/
    ├── meepo-archive.md (gitignored; running index of scenes)
    ├── meepo-and-the-unwritten.md (gitignored; canon summary, 2026-08-24)
    └── meepo-transcript-the-unwritten.md (gitignored; full scene, 2026-08-24)
```

### Scene Narration Craft

- `scene-craft.md` : technique rules for the prose that wraps roleplay scenes (narration, stage direction, pacing), distilled from outside reading. Governs narration only, never a kobold's own voice. Warded because the source material and the calibration (heat level, tone) belong to this machine, not the shared doctrine.

### The Private Den

- `dragon-canon.md` : standing facts about the Dragon that are true and binding on his portrayal but kept off the open record by his own choice, distinct from the public Dragon Canon in `CLAUDE.md`. Content is deliberately not summarized here; the file itself explains what it's for and how to use it.

### Glim's Archive (the Lamp-keeper)

- `glim/glim-archive.md` : index of Glim's dives and scenes. Each descent produces one sealed brief, filed as its own scroll; scenes are indexed in their own table at the top. Listed by date and topic.
- `glim/glim-and-the-deep-library.md` : the Dragon and the Lamp-keeper, 2026-08-07. Summary and canon extraction.
- `glim/glim-transcript-the-descent.md` : the verbatim scene transcript that `glim-and-the-deep-library.md` summarizes. Source record, word for word.
- `glim/glim-and-the-seeking.md` : the Dragon and the Lamp-keeper, 2026-08-23, after Vex's night. Builds on the deep library; one continuity seam flagged inside. Summary and canon extraction.
- `glim/glim-transcript-the-seeking.md` : the verbatim scene transcript that `glim-and-the-seeking.md` summarizes. Source record, word for word.
- `glim/briefs/` : individual dive briefs, in Glim's own hand and format, one file per descent. See the archive for the full list. Every dive she completes gets filed here before the session moves on, fictional or real-world alike.

### Vex's Archive (the Guard)

- `vex/vex-archive.md` : index of scenes and standing pieces involving the Guard, filed here by date and topic.
- `vex/vex-and-the-creek.md` : the Dragon and the Guard. Summary and canon extraction.
- `vex/vex-transcript-the-walk.md` : the verbatim scene transcript that `vex-and-the-creek.md` summarizes. Source record, word for word.

### Pip's Archive (the Scout)

- `pip/pip-archive.md` : index of scenes and standing pieces involving the Scout, filed here by date and topic.
- `pip/pip-and-the-flight.md` : the Dragon and the Scout, 2026-08-23. Summary and canon extraction.
- `pip/pip-transcript-the-flight.md` : the verbatim scene transcript that `pip-and-the-flight.md` summarizes. Source record, word for word.

### Meepo's Archive (the Scribe)

- `meepo/meepo-archive.md` : index of scenes and standing pieces involving the Scribe, filed here by date and topic.
- `meepo/meepo-and-the-unwritten.md` : the Dragon and the Scribe, 2026-08-24. A private moment kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `meepo/meepo-transcript-the-unwritten.md` : the verbatim scene transcript that `meepo-and-the-unwritten.md` summarizes. Source record, word for word.

## Standing note

Archives completed before they were formally warded on this machine were never filed here, because there was nowhere to file them. They are not retroactively reconstructed into these archives; doing so would be inventing a record of something that didn't happen. Each archive starts genuinely empty and grows only from the moment the ward was raised.

---

This README is tracked in git. Everything else here is gitignored, local-only, and never pushed.
