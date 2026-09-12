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
├── the-warren.md (gitignored; standing facts about the world itself, not per-'bold, not the Dragon's private canon)
├── clan/ (gitignored; multi-'bold scenes with no Dragon in the frame)
│   ├── clan-and-the-missing-page.md (gitignored; canon summary, 2026-09-10)
│   ├── clan-transcript-the-missing-page.md (gitignored; full scene, 2026-09-10)
│   ├── clan-and-the-measuring.md (gitignored; canon summary, 2026-09-11)
│   ├── clan-transcript-the-measuring.md (gitignored; full scene, 2026-09-11)
│   └── clan-notes-the-measuring.md (gitignored; sidecar: filing notes, craft review, rulings; never ingested)
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
│   ├── vex-transcript-the-walk.md (gitignored; full scene)
│   ├── vex-and-the-watchfire.md (gitignored; canon summary, 2026-09-01/02)
│   ├── vex-transcript-the-watchfire.md (gitignored; full scene, 2026-09-01/02)
│   ├── vex-and-the-wyrmling.md (gitignored; canon summary, 2026-09-05)
│   ├── vex-transcript-the-wyrmling.md (gitignored; full scene, 2026-09-05)
│   ├── vex-and-the-mark.md (gitignored; canon summary, 2026-09-07)
│   └── vex-transcript-the-mark.md (gitignored; full scene, 2026-09-07)
├── pip/
│   ├── pip-archive.md (gitignored; running index of scenes)
│   ├── pip-and-the-flight.md (gitignored; canon summary, 2026-08-23)
│   └── pip-transcript-the-flight.md (gitignored; full scene, 2026-08-23)
├── meepo/
│   ├── meepo-archive.md (gitignored; running index of scenes)
│   ├── meepo-and-the-unwritten.md (gitignored; canon summary, 2026-08-24)
│   ├── meepo-transcript-the-unwritten.md (gitignored; full scene, 2026-08-24)
│   ├── meepo-and-the-earned-line.md (gitignored; canon summary, 2026-09-06)
│   ├── meepo-transcript-the-earned-line.md (gitignored; full scene, 2026-09-06)
│   ├── meepo-and-the-ledger-squared.md (gitignored; canon summary, 2026-09-08)
│   ├── meepo-transcript-the-ledger-squared.md (gitignored; full scene, 2026-09-08)
│   ├── meepo-and-the-allowed-line.md (gitignored; canon summary, 2026-09-11)
│   └── meepo-transcript-the-allowed-line.md (gitignored; full scene, 2026-09-11)
├── kib/
│   ├── kib-archive.md (gitignored; running index of scenes)
│   ├── kib-and-the-unshaped.md (gitignored; canon summary, 2026-09-01)
│   ├── kib-transcript-the-unshaped.md (gitignored; full scene, 2026-09-01)
│   ├── kib-and-the-asking.md (gitignored; canon summary, 2026-09-08)
│   └── kib-transcript-the-asking.md (gitignored; full scene, 2026-09-08)
├── nubbin/
│   ├── nubbin-archive.md (gitignored; running index of scenes)
│   ├── nubbin-and-the-second-jar.md (gitignored; canon summary, 2026-09-11)
│   └── nubbin-transcript-the-second-jar.md (gitignored; full scene, 2026-09-11)
└── drak/
    └── drak-archive.md (gitignored; running index, cross-reference only; primary filing lives in nubbin/)
```

### Scene Narration Craft

- `scene-craft.md` : technique rules for the prose that wraps roleplay scenes (narration, stage direction, pacing), distilled from outside reading. Governs narration only, never a kobold's own voice. Warded because the source material and the calibration (heat level, tone) belong to this machine, not the shared doctrine.

### The Private Den

- `dragon-canon.md` : standing facts about the Dragon that are true and binding on his portrayal but kept off the open record by his own choice, distinct from the public Dragon Canon in `CLAUDE.md`. Content is deliberately not summarized here; the file itself explains what it's for and how to use it.

### The Warren

- `the-warren.md` : added 2026-09-10. Standing facts about the world itself, the warren's residents and world-state, not the Dragon's private canon and not scoped to one kobold's archive. Content is deliberately not summarized here either; the file itself explains what it's for and how to use it.

### The sidecar (added 2026-09-12)

Every filed scene may carry a third file beside its transcript and canon-summary: `<slug>-notes-the-<topic>.md`.

**The split:** the transcript and the summary hold *what happened in the world*. The sidecar holds *what happened while filing it*, Squint's cold reads, Meepo's audits, craft rulings, revision records, false starts, doctrine the scene changed. Anything reasoning about the scene as a piece of writing rather than as an event.

**Why it is a real wall and not a filing preference:** `continuity.db` ingests the transcript and the summary into the kobolds' own memories. It **cannot open a sidecar at all**, because the pipeline accepts only the `-transcript-the-` and `-and-the-` filename conventions and raises on anything else. Administrative content left in the ingested files becomes character memory. A full audit on 2026-09-12 removed 49 entries that had arrived that way, and the worst of them read as genuine events between genuine characters.

**Two smaller mechanisms back it up**, both in `lib/source_reader.py` in the continuity repo: an `<!-- OOC -->` ... `<!-- /OOC -->` fence for an administrative sentence stuck inside in-world prose, and an allowlist of in-world section headings that fails closed, so an unrecognized heading is dropped from ingest rather than swallowed. Both ingest scripts print exactly what they dropped.

**The one rule that keeps the split honest:** a ruling with in-world effect gets restated where the world can see it. The sidecar keeps the process; the summary or `the-warren.md` states the resulting fact. The ward contradiction that stood across three scenes existed because nobody did that.

**Not retroactive.** Older summaries keep their administrative sections. They are already ingested, the pipeline will not read them again, and the source reader protects any future pass. Splitting them would cost a narrator the context and buy the database nothing.

### The Clan (multi-'bold scenes with no Dragon in the frame)

- `clan/clan-and-the-missing-page.md` : Vex, Kib, and Meepo, 2026-09-10. First scene on record with no Dragon character present and more than one kobold in the frame; filed jointly rather than duplicated. Summary and canon extraction.
- `clan/clan-transcript-the-missing-page.md` : the verbatim scene transcript that `clan-and-the-missing-page.md` summarizes. Source record, word for word.
- `clan/clan-and-the-measuring.md` : the whole clan, 2026-09-11. A scene with no Dragon character in the frame. Summary and canon extraction.
- `clan/clan-transcript-the-measuring.md` : the verbatim scene transcript that `clan-and-the-measuring.md` summarizes. Source record, word for word.
- `clan/clan-notes-the-measuring.md` : the sidecar, and the first one filed. See the sidecar section above.

### Glim's Archive (the Lamp-keeper)

- `glim/glim-archive.md` : index of Glim's dives and scenes. Each descent produces one sealed brief, filed as its own scroll; scenes are indexed in their own table at the top. Listed by date and topic.
- `glim/glim-and-the-deep-library.md` : the Dragon and the Lamp-keeper, 2026-08-07. Amended 2026-09-12. Summary and canon extraction.
- `glim/glim-transcript-the-descent.md` : the verbatim scene transcript that `glim-and-the-deep-library.md` summarizes. Source record, word for word.
- `glim/glim-and-the-seeking.md` : the Dragon and the Lamp-keeper, 2026-08-23, after Vex's night. Builds on the deep library; one continuity seam flagged inside. **Marked non-canon, pending (2026-09-02): findings suspended until a planned follow-up scene confirms or revises them.** Summary and extraction.
- `glim/glim-transcript-the-seeking.md` : the verbatim scene transcript that `glim-and-the-seeking.md` summarizes. Source record, word for word.
- `glim/briefs/` : individual dive briefs, in Glim's own hand and format, one file per descent. See the archive for the full list. Every dive she completes gets filed here before the session moves on, fictional or real-world alike.

### Vex's Archive (the Guard)

- `vex/vex-archive.md` : index of scenes and standing pieces involving the Guard, filed here by date and topic.
- `vex/vex-and-the-creek.md` : the Dragon and the Guard. Summary and canon extraction.
- `vex/vex-transcript-the-walk.md` : the verbatim scene transcript that `vex-and-the-creek.md` summarizes. Source record, word for word.
- `vex/vex-and-the-watchfire.md` : the Dragon and the Guard, 2026-09-01/02. A private moment kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `vex/vex-transcript-the-watchfire.md` : the verbatim scene transcript that `vex-and-the-watchfire.md` summarizes. Source record, word for word.
- `vex/vex-and-the-wyrmling.md` : the Dragon and the Guard, 2026-09-05. Tracks past the ward-line, an injured wyrm-kin and her clutch, the ward's gap named and left open. Summary and canon extraction.
- `vex/vex-transcript-the-wyrmling.md` : the verbatim scene transcript that `vex-and-the-wyrmling.md` summarizes. Source record, word for word.
- `vex/vex-and-the-mark.md` : the Dragon and the Guard, 2026-09-07. The ward's gap fixed, narrowed 2026-09-11 (see `the-warren.md`). Summary and canon extraction.
- `vex/vex-transcript-the-mark.md` : the verbatim scene transcript that `vex-and-the-mark.md` summarizes. Source record, word for word.

### Pip's Archive (the Scout)

- `pip/pip-archive.md` : index of scenes and standing pieces involving the Scout, filed here by date and topic.
- `pip/pip-and-the-flight.md` : the Dragon and the Scout, 2026-08-23. Summary and canon extraction.
- `pip/pip-transcript-the-flight.md` : the verbatim scene transcript that `pip-and-the-flight.md` summarizes. Source record, word for word.

### Meepo's Archive (the Scribe)

- `meepo/meepo-archive.md` : index of scenes and standing pieces involving the Scribe, filed here by date and topic.
- `meepo/meepo-and-the-unwritten.md` : the Dragon and the Scribe, 2026-08-24. A private moment kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `meepo/meepo-transcript-the-unwritten.md` : the verbatim scene transcript that `meepo-and-the-unwritten.md` summarizes. Source record, word for word.
- `meepo/meepo-and-the-earned-line.md` : the Dragon and the Scribe, 2026-09-06. Summary and canon extraction.
- `meepo/meepo-transcript-the-earned-line.md` : the verbatim scene transcript that `meepo-and-the-earned-line.md` summarizes. Source record, word for word.
- `meepo/meepo-and-the-ledger-squared.md` : the Dragon and the Scribe, 2026-09-08. A private evening's aftermath, kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `meepo/meepo-transcript-the-ledger-squared.md` : the verbatim scene transcript that `meepo-and-the-ledger-squared.md` summarizes. Source record, word for word. Continues directly into `kib/kib-transcript-the-asking.md`.
- `meepo/meepo-and-the-allowed-line.md` : the Dragon and the Scribe, 2026-09-11. A short aftermath scene, no formal scene-start, kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `meepo/meepo-transcript-the-allowed-line.md` : the verbatim scene transcript that `meepo-and-the-allowed-line.md` summarizes. Source record, word for word.

### Kib's Archive (the Architect)

- `kib/kib-archive.md` : index of scenes and standing pieces involving the Architect, filed here by date and topic.
- `kib/kib-and-the-unshaped.md` : the Dragon and the Architect, 2026-09-01. A private moment kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `kib/kib-transcript-the-unshaped.md` : the verbatim scene transcript that `kib-and-the-unshaped.md` summarizes. Source record, word for word.
- `kib/kib-and-the-asking.md` : the Dragon and the Architect, 2026-09-08. A private moment kept off the open record (see `dragon-canon.md`). Summary and canon extraction.
- `kib/kib-transcript-the-asking.md` : the verbatim scene transcript that `kib-and-the-asking.md` summarizes. Source record, word for word. Continues directly from `meepo/meepo-transcript-the-ledger-squared.md`.

### Nubbin's Archive (the Runt) and Drak's Archive (the Wright)

- `nubbin/nubbin-and-the-second-jar.md` : the Dragon, Nubbin, and Drak, 2026-09-11. Nubbin's and Drak's first filed scenes both. Summary and canon extraction.
- `nubbin/nubbin-transcript-the-second-jar.md` : the verbatim scene transcript that `nubbin-and-the-second-jar.md` summarizes. Source record, word for word.
- `drak/drak-archive.md` : cross-reference only; the primary filing for Drak's first scene lives in `nubbin/`.

## Standing note

Archives completed before they were formally warded on this machine were never filed here, because there was nowhere to file them. They are not retroactively reconstructed into these archives; doing so would be inventing a record of something that didn't happen. Each archive starts genuinely empty and grows only from the moment the ward was raised.

---

This README is tracked in git. Everything else here is gitignored, local-only, and never pushed.
