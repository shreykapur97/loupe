# Setup

Tools and first run on a new machine.

Shape: install → first-time setup → daily commands → troubleshooting.

## 1. Install

| Tool | Why | Install | Check |
|---|---|---|---|
| Git | repo | `winget install Git.Git` | `git --version` |
| .NET SDK (8) | build | `winget install Microsoft.DotNet.SDK.8` | `dotnet --version` |
| VS Code + C# Dev Kit | editor, debugging | `winget install Microsoft.VisualStudioCode`, then the C# Dev Kit extension | — |
| Rhino 8 | run and test | rhino3d.com | — |

Visual Studio is not needed.

## 2. First-time setup

```
git clone https://github.com/shreykapur97/loupe.git
cd loupe
dotnet new install Rhino.Templates
```

`src/Loupe/` came from `dotnet new grasshopper -p:v 8`, cut down to one
target (ADR 0001). `Rhino.Templates` is only needed to scaffold something
new.

### Loading the plugin in Rhino

Once, in Rhino: run `GrasshopperDeveloperSettings`, add
`src/Loupe/bin/Debug/net7.0-windows/`, restart Rhino. (Or copy the `.gha`
to the Libraries folder.)

## 3. Daily commands

Same table as CLAUDE.md → Commands.

| What | Where | Command |
|---|---|---|
| Build | repo root | `dotnet build` |
| Debug | VS Code | Run → "Rhino 8" or "Attach to Rhino 8" (`.vscode/launch.json`) |

Every rebuild needs a Rhino restart.

## 4. Icons

The panel buttons are Lucide icons (ADR 0008), rendered to 48 px PNGs in
`src/Loupe/Resources/Icons/`. To change or add one: add its name to the
list in `tools/icons/render.js`, then, with Node and Playwright installed
and the `lucide-static` package unpacked somewhere:

```
node tools/icons/render.js <lucide-static>/icons src/Loupe/Resources/Icons
```

Every PNG under `Resources/` is embedded automatically; load it with
`Assets.Load("<name>")`.

## 5. Releasing

CI (`.github/workflows/build.yml`) builds the `.gha` on Windows on every
push; the build is under the run's Artifacts. A tag starting with `v`
also makes a GitHub Release with `Loupe.gha` attached, which is what users
download (README → Install). A tag with `-` (`v0.1.0-alpha`) is marked
pre-release.

```
git tag v0.1.0-alpha
git push origin v0.1.0-alpha
```

Or without a local tag: GitHub → Actions → build → Run workflow, with the
tag filled in.

## 6. Troubleshooting

| Symptom | Fix |
|---|---|
| Rhino says the `.gha` is blocked | Right-click → Properties → Unblock (downloaded files only) |
| Build fails: file in use | Close Rhino; it locks the loaded `.gha` |
| No Loupe button | Grasshopper: View → Canvas Toolbar must be on. Rhino command history: "Loupe: button added…" means it loaded; "canvas toolbar not found" means `ToolbarButton.cs` didn't find the strip; neither means the plugin didn't load |
| Plugin not loaded | Rhino 8 must run on .NET Core (default), not `/netfx` (ADR 0001) |
