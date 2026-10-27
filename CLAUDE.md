# Loupe

Grasshopper plugin (`.gha`) for Rhino 8. Adds a button to the canvas toolbar
that opens a second, resizable window with its own navigable canvas on the
same document, so two parts of one script can be viewed at once. Named after
the jeweller's magnifier. Repo `shreykapur97/loupe`. Plan: `.docs/phases.md`.

## How the notes work

| Where | What | Loaded |
|---|---|---|
| `CLAUDE.md` | Map, build order, commands, hard rules, working together | every session |
| `.claude/rules/*.md` | Habits by area (plugin, docs) | when files matching their `paths` are touched |
| `.claude/skills/*` | Repeatable workflows (`/wrap-up`) | when invoked |
| `.claude/settings.json` | Permissions, hooks | always (by Claude Code) |
| `.docs/status/shrey.md` | Now / Next — rewritten by `/wrap-up`, never appended | every session (imported below) |
| `.docs/phases.md` | Decided, To confirm, build phases, feature tracker, Open | on demand |
| `.docs/requirements.md` | Non-functional needs | on demand |
| `.docs/architecture.md` | How the plugin hooks into Grasshopper, repo layout, known quirks | on demand |
| `.docs/setup.md` | Tools and new-machine setup | on demand |
| `.docs/case-study.md` | Research: plugins, APIs, name checks, and findings | on demand |
| `.docs/adr/` | One file per decision and why, written as decisions happen; index in `adr/_index.md` | on demand |
| `.docs/sessions/shrey/` | One record per session (`/wrap-up` writes it) | on demand |

New habit → the matching rule file. New tool → setup.md + Commands + settings.json
(+ an ADR if it was a choice). History → git, not the docs.

## Build order

Build bottom-up. Don't start a layer until the one below works and has been
checked in Rhino (a rebuild needs a Rhino restart).

| # | Layer | Where | Rule file |
|---|---|---|---|
| 1 | Scaffold: template, layout, builds a `.gha` | `src/Loupe/` | plugin.md |
| 2 | Loader: runs at load, waits for the editor | `src/Loupe/LoupeLoader.cs` | plugin.md |
| 3 | Toolbar button appears | `src/Loupe/UI/ToolbarButton.cs` | plugin.md |
| 4 | Window with the second canvas, toggle behaviour (ADR 0002) | `src/Loupe/UI/LoupeWindow.cs` | plugin.md |
| 5 | Sync and cleanup: refresh, follow the active document, detach | `src/Loupe/UI/LoupeWindow.cs` | plugin.md |

## Commands

Not scaffolded yet (`phases.md` → Phase 0).

| What | Where | Command |
|---|---|---|
| Install templates (once) | anywhere | `dotnet new install Rhino.Templates` |
| Build | repo root | `dotnet build` → `.gha` in `src/Loupe/bin/` |
| Load in Rhino | Rhino | `GrasshopperDeveloperSettings` → add the `bin` folder, restart Rhino |

## Hard rules

Change only through an ADR.

- Rhino 8 only (ADR 0001). No Rhino 7 / net48 target.
- A compiled plugin, never a script component (it would re-add the button on
  every solve).
- Only `UI/ToolbarButton.cs` touches editor internals. Find the toolbar by
  type and position, never by fixed indices like `Controls[0].Controls[1]`.
- The second canvas is a viewer and navigator first; don't rely on wire
  dragging, the search popup or widgets working in it.
- A canvas's `Document` is set to null before the canvas is disposed.
- Never commit `bin/`, `obj/` or built `.gha` files.

## Comment style

No comments unless the code can't say it. Code as short as it can be: no
boilerplate, nothing unused.

## Working together

One developer: Shrey (`<who>` = `shrey`, git user.name `Shrey Kapur`).

- Explanations brief, about what is happening and what runs.
- No code in chat unless Shrey says "give me the code" / "write the code";
  then as short as possible.
- Don't overwrite existing files unless asked.
- Git workflow: TODO (`phases.md` → Open).

@.docs/status/shrey.md
