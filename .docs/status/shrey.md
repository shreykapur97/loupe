# Status — Shrey

Where Shrey is and what's next. Rewritten by `/wrap-up`, one screen max.
Plan: `phases.md`.

## Now

- Notes, README, GPL-3.0 licence in place. ADRs 0001–0004 (Rhino 8 /
  `net7.0-windows` only, window toggle, licence, name).
- **Code written, not run in Rhino:** loader, toolbar button, Loupe window
  with the second canvas, hide / show, redraw sync, document follow,
  cleanup (`src/Loupe/`). Compiles against the Grasshopper 8 NuGet package
  (checked on Linux with a net7.0 copy); the real `-windows` build only
  runs on Windows or in CI.

## Next

1. On Windows: `dotnet build`, load via `GrasshopperDeveloperSettings`,
   restart Rhino. Check: button shows on the canvas toolbar, window opens
   bottom-left, zoom / pan independent, toggle and X hide it, switching
   files follows.
2. Fix what doesn't work there (the toolbar search is the likeliest).
3. Decide the git workflow; merge this branch into `main` (`phases.md` →
   Open).
4. Claim the Package Manager name with a first release (ADR 0004).
