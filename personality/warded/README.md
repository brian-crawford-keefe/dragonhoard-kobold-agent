# The Ward (Glim's local archive)

**Provenance note, stated plainly:** this README did not exist anywhere before 2026-08-10, on this machine or in the repo. `.claude/agents/glim.md` was edited elsewhere to point at `personality/warded/README.md` "for the ward," but no such file was ever committed, and the machine that wrote that line kept its own copy local, exactly as the convention it was describing intends. This document is authored fresh, to match the intent of Glim's scroll, not recovered from any prior source. Nothing below is a rediscovered original.

## What this is

Every dive Glim actually completes and grades, whether the Dragon asked her directly or another 'bold dispatched her, gets filed here **before** she reports the finished brief. One file per descent, in `briefs/`, indexed by date and topic in `glim-archive.md`.

This is not the same thing as her **unlit pages** (documented in her own scroll). The unlit pages hold what she could not grade: a feeling, a contradiction she never resolved, a thread with insufficient basis to rule on. The ward holds the opposite: everything she *did* confirm, filed in full, every single time, with no exception and no waiting to be asked. The unlit pages are about honest incompleteness. The ward is about not losing completed work.

## Why it stays off the shared record

The hoard (this repo, the doctrine, the engine, `dialog.json`) is the clan's shared, permanent memory: things every session on every machine should see. A research dive is a working artifact of *this* machine, on *this* date, chasing *this* question. It may be superseded within days, it may reference something specific to whoever's running the session, and it will accumulate without bound if nothing ever ages out. Committing every dive would slowly turn the hoard into a research diary nobody asked to carry. So the archive stays local: real, durable, and reread on this machine, but never pushed, never a source of drift between sessions the way the doctrine scrolls are.

## Structure

```
personality/warded/
├── README.md              (tracked in git; you're reading it)
├── glim-archive.md         (gitignored; the running index)
└── briefs/                 (gitignored; one file per descent)
    └── YYYY-MM-DD-topic-slug.md
```

- **Brief filenames:** `YYYY-MM-DD-topic-slug.md`, the date of the descent and a short slug of the subject. Content is whatever Glim actually delivered: the graded answer, anchors, confidence and gaps, and (per her scroll) her own first-person phrasing and any closing note.
- **`glim-archive.md`:** one line per brief, date + topic + a short pointer, newest first. Not a second copy of the content, just enough to find the right file later without reopening all of them.
- **Both are gitignored** (see the repo's `.gitignore`). A fresh clone of this repo starts with an empty archive and this README. That's expected, not a loss: the ward was never meant to travel.

## Standing note

Dives completed before this ward existed (on this machine) were never filed here, because there was nowhere to file them. They are not retroactively reconstructed into this archive; doing so would be inventing a record of something that didn't happen. The archive starts genuinely empty as of 2026-08-10 and grows only from here forward.
