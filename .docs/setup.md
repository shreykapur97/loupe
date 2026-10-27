# Setup

Tools and first run on a new machine.

Shape: install → first-time setup → making a change → icons → releasing →
troubleshooting.

## 1. Install

| Tool | Why | Install | Check |
|---|---|---|---|
| Git | repo | `winget install Git.Git` | `git --version` |
| .NET SDK (8) | build | `winget install Microsoft.DotNet.SDK.8` | `dotnet --version` |
| VS Code | editor | `winget install Microsoft.VisualStudioCode` | — |
| C# Dev Kit (optional) | IntelliSense, debugging | `code --install-extension ms-dotnettools.csdevkit` | — |
| Rhino 8 | run and test | rhino3d.com | — |

Installs run from any folder. Restart VS Code after installing the SDK so
the terminal finds `dotnet`. Visual Studio, Node and a Python venv are not
needed (Node only for Icons).

## 2. First-time setup

1. Clone into the folder you're in (the `.` stops a nested `loupe/loupe`):

   ```
   git clone https://github.com/shreykapur97/loupe.git .
   ```

2. Build from the repo root: `dotnet build` →
   `src/Loupe/bin/Debug/net7.0-windows/Loupe.gha`.
3. Load it in Rhino (once): run `GrasshopperDeveloperSettings`, add the
   full path of `src/Loupe/bin/Debug/net7.0-windows/`, OK, restart Rhino.
   (Or copy the `.gha` to the Libraries folder.)
4. Open Grasshopper: the Loupe button is on the canvas toolbar.

`src/Loupe/` came from `dotnet new grasshopper -p:v 8`, cut down to one
target (ADR 0001). `dotnet new install Rhino.Templates` is only needed to
scaffold something new.

## 3. Making a change

From the repo root:

1. Edit under `src/Loupe/`.
2. `dotnet build` (close Rhino first if it says the file is in use).
3. Restart Rhino, open Grasshopper, check the change.
4. Commit and push:

   ```
   git status
   git add -A
   git commit -m "What changed"
   git push
   ```

   CI builds every push (GitHub → Actions).

Debug instead of step 3: VS Code → Run → "Rhino 8" or "Attach to Rhino 8"
(`.vscode/launch.json`).

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

1. Set `<Version>` in `src/Loupe/Loupe.csproj` and add the entry to the
   top of `CHANGELOG.md` (fix → `0.4.1`, feature → `0.5.0`; keep `-alpha`
   until 1.0).
2. Commit and push that.
3. Tag and push the tag:

   ```
   git tag v0.5.0-alpha
   git push origin v0.5.0-alpha
   ```

4. A few minutes later: GitHub → Releases has it with `Loupe.gha`.

If the release build fails, delete the tag, fix, tag again:
`git tag -d v0.5.0-alpha` and `git push origin :v0.5.0-alpha`.

CI (`.github/workflows/build.yml`) builds the `.gha` on Windows on every
push; the build is under the run's Artifacts. A tag starting with `v`
also makes a GitHub Release with `Loupe.gha` attached, which is what users
download (README → Install). A tag with `-` (`v0.1.0-alpha`) is marked
pre-release.

Or without a local tag: GitHub → Actions → build → Run workflow, with the
tag filled in.

## 6. Troubleshooting

User-side problems (no button, can't move it…): README → Troubleshooting.

| Symptom | Fix |
|---|---|
| Rhino says the `.gha` is blocked | Right-click → Properties → Unblock (downloaded files only) |
| Build fails: file in use | Close Rhino; it locks the loaded `.gha` |
