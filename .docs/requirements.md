# Requirements

What Loupe must do and how well. Features are in `phases.md`; this file
covers everything else.

Marked **decided**, **assumed** (Claude's default, confirm or change), or
**open**.

## Users and use

- Grasshopper users on Rhino 8 who work on large definitions. **Decided**
  (ADR 0001).
- Windows only for now. **Assumed** (`phases.md` → Open, Mac).
- The second canvas is mainly for viewing and navigating. **Decided**
  (CLAUDE.md → Hard rules).

## Behaviour

- Zoom and pan in the Loupe window never move the main canvas, and the
  other way round. **Decided.**
- Selection, wires and solutions show the same in both canvases. **Decided.**
- The panel follows the active document. **Decided.**
- No setting or file is written to the user's machine. **Assumed.**

## Performance

- No noticeable slowdown of solves or main-canvas redraws with the window
  open or hidden. **Assumed.**
- A hidden window does no redraw work. **Assumed.**

## Robustness

- If the toolbar can't be found (a future Grasshopper version), Loupe
  fails quietly: no button, no crash, a line in the Rhino command history.
  **Assumed.**
- No leaks: closing documents or Grasshopper leaves no handlers or canvases
  behind. **Decided.**

## Licence

- GPL-3.0. **Decided** (ADR 0003).
