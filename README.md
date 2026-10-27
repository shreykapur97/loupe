# <img src="src/Loupe/Resources/loupe_24.png" width="24" alt=""> Loupe

**A second canvas for Grasshopper.** Keep one part of your definition in
view while you work on another.

Loupe adds one button to the Grasshopper canvas toolbar. Click it and a
small framed view opens in the corner of the canvas, showing exactly what
you're looking at. Then move around the main canvas as usual: Loupe keeps
its view, so you can watch the inputs while you edit the outputs, or keep
an eye on a panel at the far end of a big script.

It's a live view of the same document, not a copy: solutions, selections
and changes show in both.

Named after the jeweller's magnifier.

> **Alpha.** Loupe is new and has only been tested on a few machines.
> Please [report problems](https://github.com/shreykapur97/loupe/issues).

## Requirements

- Rhino 8 for Windows, Grasshopper 1
- Rhino started normally (Loupe doesn't load when Rhino runs with
  `/netfx`)

## Install

1. Download `Loupe.gha` from
   [Releases](https://github.com/shreykapur97/loupe/releases) (the newest
   one at the top).
2. Right-click the file → **Properties** → tick **Unblock** → **OK**.
   Windows blocks downloaded plugins, and Grasshopper won't load a blocked
   one.
3. In Grasshopper: **File → Special Folders → Components Folder**, and
   drop `Loupe.gha` in there.
4. Restart Rhino.

The Loupe button appears at the right end of the canvas toolbar (the strip
under the component tabs, with save, zoom and named views).

**Update:** replace `Loupe.gha` with the new one, unblock it, restart
Rhino. **Uninstall:** delete `Loupe.gha` from that folder and restart.

## Use

Click <img src="src/Loupe/Resources/loupe_24.png" width="16" alt="Loupe">
on the canvas toolbar to show Loupe; click again to hide it. It opens in
the bottom-left corner at the main canvas's current view.

The buttons down Loupe's left edge, top to bottom:

| | Button | What it does |
|---|---|---|
| <img src="src/Loupe/Resources/Icons/pin.png" width="16" alt=""> | **Pin** | On (default): Loupe sits fixed on the canvas. Off: it floats in its own window: drag its frame to move it anywhere (another monitor too), a corner to resize it. Pin it again over the canvas to drop it there. |
| <img src="src/Loupe/Resources/Icons/house.png" width="16" alt=""> | **Home** | Pinned, default size, bottom-left. |
| <img src="src/Loupe/Resources/Icons/eye.png" width="16" alt=""> | **View only** | On (default): Loupe is frozen: no editing, no pan or zoom, so nothing changes by accident. Off: pan, zoom and edit in it like the main canvas. |
| <img src="src/Loupe/Resources/Icons/scan.png" width="16" alt=""> | **Match view** | Loupe jumps to what the main canvas shows right now. |

A typical flow: go to the part you want to keep an eye on, click **Match
view**, then work elsewhere on the main canvas.

Closing the Grasshopper editor hides Loupe. Nothing is saved between
sessions: it opens fresh each time.

## Good to know

- With view only off you can edit in Loupe, but it's built for viewing:
  wire dragging, the search popup and some widgets may not behave as on
  the main canvas.
- Loupe has no compass or zoom bar of its own; the main canvas keeps
  them.
- One Loupe at a time.

## Troubleshooting

| Problem | Try |
|---|---|
| No Loupe button | Grasshopper: **View → Canvas Toolbar** must be on. |
| Still no button, and Rhino's command history has no Loupe line | It didn't load: unblock the file (Install, step 2) and make sure Rhino isn't started with `/netfx`. |
| Command history says "canvas toolbar not found" | Please [open an issue](https://github.com/shreykapur97/loupe/issues) with your Rhino version. |
| Can't move or resize it | Unpin it, then drag the dark frame or a corner; pin it again where you want it. |
| Can't pan or zoom in it | Turn **View only** off, or use **Match view**. |

## Build from source

Needs the .NET 8 SDK on Windows. `dotnet build` at the repo root gives
`src/Loupe/bin/Debug/net7.0-windows/Loupe.gha`. Details:
[`.docs/setup.md`](.docs/setup.md); how it works:
[`.docs/architecture.md`](.docs/architecture.md).

## Licence

[GPL-3.0](LICENSE). You can use, change and share Loupe; anything you
distribute that's based on it must be shared under the same licence, with
its source.

Button icons: [Lucide](https://lucide.dev), ISC licence
([`LICENSE-lucide.txt`](src/Loupe/Resources/Icons/LICENSE-lucide.txt)).
