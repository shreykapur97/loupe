# Case study

Plugins, APIs and checks looked at before deciding things. References, not
decisions; decisions go in an ADR.

Copy ideas, never code or assets.

## To study

- Rhino Package Manager: is "Loupe" taken?
- Other Grasshopper plugins that add toolbar buttons or extra canvases, and
  how they find the toolbar.
- `GH_Canvas` / `GH_Viewport` behaviour with more than one canvas on a
  document.

## Findings

What was looked at, and what was found. Dated.

### 2026-10-27 — Name check (Shrey, from the handover)

- No Grasshopper plugin called Loupe on food4Rhino or GitHub.
- Rhino Package Manager not checked: the cloud session's network can't
  reach `yak.rhino3d.com` (403). Check with `PackageManager` in Rhino, or
  open `https://yak.rhino3d.com/packages/loupe` (a 404 should mean free).
