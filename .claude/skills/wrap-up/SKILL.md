---
name: wrap-up
description: End of a work session - check every doc, update only what the session changed, report the list
disable-model-invocation: true
---

Pull first (`git pull`) so the docs are current. Then check every doc below
against what happened this session. Update only the ones the session actually
changed; leave the rest alone. Don't commit — Shrey does.

| If this happened | Update |
|---|---|
| Always | `.docs/status/shrey.md` — rewrite (not append), one screen max: Now, Next |
| Always | `.docs/sessions/shrey/YYYY-MM-DD-<topic>.md` — record of the conversation: how it went (numbered steps, Shrey's words where they set direction), explanations worth rereading, what was left open, and the session name/ID. Same session continued → add to its file |
| Something was decided | new `.docs/adr/NNNN-*.md`, its row in `.docs/adr/_index.md`; remove it from Open in `.docs/phases.md` |
| Something new became undecided | add it to Open in `.docs/phases.md` |
| A feature moved forward | the feature tracker in `.docs/phases.md` |
| A new habit or rule | the matching `.claude/rules/*.md` |
| A new tool or command | `.docs/setup.md`, CLAUDE.md Commands, `.claude/settings.json` |
| Structure or how it hooks into Grasshopper changed | `.docs/architecture.md` |
| Requirements decided | `.docs/requirements.md` |

Rules:

- History goes to git, not the docs. Docs say what is true now.
- One fact in one place: link to the file that owns it, don't copy it.
- Never write secrets or tokens into any doc.

End with a short list: each file changed and one line on why, and anything
left unfinished.
