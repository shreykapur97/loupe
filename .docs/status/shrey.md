# Status — Shrey

Where Shrey is and what's next. Rewritten by `/wrap-up`, one screen max.
Plan: `phases.md`.

## Now

- v0.2.0-alpha works in Rhino 8: panel on the canvas, lock, home, toggle,
  no compass / Markov bar.
- v0.3.0-alpha written, not yet tried (ADR 0007, 0008): panel-style frame,
  five Lucide buttons down the left (lock, home, float, view only, match
  view), floating window, view only on by default, opens at the main
  canvas's view, hides when the editor closes. Code split into one class
  per job (`plugin.md` → Layout).

## Next

1. Try v0.3.0-alpha: frame, each button, float and dock back, view only
   blocks everything, match view, editor close hides it, high-DPI.
2. Decide the git workflow; merge this branch into `main` (`phases.md` →
   Open).
3. Menu item / shortcut, or a Rhino command (`.rhp`) — not decided.
4. Claim the Package Manager name with a first release (ADR 0004).
