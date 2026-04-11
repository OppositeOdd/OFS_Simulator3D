A Fork of Falafel's fork of OpenFunScripter's OFS_Simulator3D that fixes some issues.

<img src="https://github.com/ZestyRaraferu/OFS_Simulator3D/blob/1.2.1/favicon.png" width="128">

# OFS_Simulator3D
A 3D simulator for OFS made in godot.

This project uses godot 3.5.3 with C# mono support.

## Changes made in this fork:

- Customized model to be an adult toy instead of a cylinder.

- Added a help window to display available hotkey commands, toggled with 'H'

- Added rotational support on all 3 axes to view from different angles.

  - Y-Axis +- 15 degrees Left and Right DPad
  - X-Axis +- 15 degrees Up and Down DPad
  - Z-Axis +- 15 degrees Shift + Left/Right DPad
  - Reset to default position using 'R'

- Added more visual twist reference guides to the front and back of the main stroke simulator.

- Added A1 and A0 funscript channel T-Valve support in the form of a bar gauge, toggled with 'V'.

- Copied logic for valve control from "https://github.com/jcfain/TCodeESP32" under MotorHandler0_4.h lines 370-428

**Credits for the model:** *Real Fake Vagina model by [CycloneRed2](https://twitter.com/CycloneRed2) smutbase original post [here](https://smutba.se/project/ec58b65e-f574-419a-96eb-be999a79238b/)*

## Model Setup

The 3D model and textures are **not included** in this repository to respect the original creator's distribution rights. To set up the model:

1. Download the model from the [original smutbase post](https://smutba.se/project/ec58b65e-f574-419a-96eb-be999a79238b/) both the CycloneRed-Real_Fake_Vagina-V1.1.3.blend.zip  and textures_eCTlmLQ.rar archives.
2. Extract the `.blend` and textures file archives and open the .blend file in Blender
3. Highlight the 3 model parts, join them with J, then ctrl+a, select rotation&scale
4. Export as `.glb` under file --> export --> glTF 2.0 --> Include "Selected Objects" --> Export
5. Place the exported `.glb`, the generated `.material` files, and the `textures/` folder into a folder named `StrokerModelCycloneRed2/` in the project root
6. Open the project in Godot — it will auto-import the assets

The folder structure should look like:
```
StrokerModelCycloneRed2/
├── CycloneRed-Real_Fake_Vagina-V1.1.3.glb
├── Fleshlight_body.material
├── Fleshlight_end.material
├── Fleshlight_vagina_insert_new.material
├── eye inside L_001.material
├── eye inside R_001.material
├── eye stroke_001.material
└── textures/
    └── (all texture .jpg/.png files)
```

> **Note:** This is only required if you're rebuilding from source, the executable will still work fine without these extra steps if you only want the simulator.
