A Fork of ZestyRaraferu's version of OpenFunScripter's OFS_Simulator3D.

This was made to be compatible with AppleSilicon ARM64 Architecture. Godot was also updated to the latest version

### macOS first run (unsigned build)
This app isn’t notarized. On first launch, macOS will block it.

After moving the .app bundle into "~/Applications"

**Option A – GUI (one time):**
1. Right-click the app → **Open**.
2. MacOS will block it, go to System Settings > Privacy and Security > Open Anyway

**Option B – Terminal (remove quarantine):**
```bash
xattr -dr com.apple.quarantine "/Applications/FunscriptSimulator3D.app"
```
<img src="https://github.com/ZestyRaraferu/OFS_Simulator3D/blob/1.2.1/favicon.png" width="128">

# OFS_Simulator3D
A 3D simulator for OFS made in godot.

This project uses godot 4.5.1 with C# mono support. 
