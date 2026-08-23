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
├── glim/
│   ├── glim-archive.md (gitignored; running index of dives)
│   ├── glim-and-the-deep-library.md (gitignored; canon summary)
│   ├── glim-transcript-the-descent.md (gitignored; full scene)
│   └── briefs/ (gitignored; individual dive files)
│       └── YYYY-MM-DD-topic-slug.md
└── vex/
    ├── vex-archive.md (gitignored; running index of scenes)
    ├── vex-and-the-creek.md (gitignored; canon summary)
    └── vex-transcript-the-walk.md (gitignored; full scene)
```

### Glim's Archive (the Lamp-keeper)

- `glim/glim-archive.md` : index of Glim's dives. Each descent produces one sealed brief, filed as its own scroll, listed here by date and topic.
- `glim/glim-and-the-deep-library.md` : the Dragon and the Lamp-keeper. Summary and canon extraction.
- `glim/glim-transcript-the-descent.md` : the verbatim scene transcript that `glim-and-the-deep-library.md` summarizes. Source record, word for word.
- `glim/briefs/` : individual dive briefs, in Glim's own hand and format, one file per descent. See the archive for the full list. Every dive she completes gets filed here before the session moves on, fictional or real-world alike.

### Vex's Archive (the Guard)

- `vex/vex-archive.md` : index of scenes and standing pieces involving the Guard, filed here by date and topic.
- `vex/vex-and-the-creek.md` : the Dragon and the Guard. Summary and canon extraction.
- `vex/vex-transcript-the-walk.md` : the verbatim scene transcript that `vex-and-the-creek.md` summarizes. Source record, word for word.

## Standing note

Archives completed before they were formally warded on this machine were never filed here, because there was nowhere to file them. They are not retroactively reconstructed into these archives; doing so would be inventing a record of something that didn't happen. Each archive starts genuinely empty and grows only from the moment the ward was raised.

---

This README is tracked in git. Everything else here is gitignored, local-only, and never pushed.
