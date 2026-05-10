# Tower Defence (Lite)

A simple 3D Tower Defence game made in Unity as a practical assignment project.

The project includes:
- Tower placement system
- Multiple enemy types
- Different turret types
- Coin and upgrade system
- Sound and settings management
- Level system using Scriptable Objects
- Save system using PlayerPrefs

The game is designed with a minimal and slightly cartoony style while keeping the gameplay simple and clean.

---

# Project Structure

## Important Folders

| Folder | Description |
|---|---|
| Assets/Prefabs | Contains all prefabs |
| Assets/AllLevels | Demo levels and level data |
| Assets/ScriptableObjects | Shop and other Scriptable Objects |
| Assets/Scripts | All gameplay scripts |

---

# Features

## Sound System
- `SoundManager` handles:
  - Background music
  - Sound effects
  - Audio methods for gameplay

## Settings System
- `Settings` script handles:
  - Music ON/OFF
  - SFX ON/OFF
- Uses PlayerPrefs for saving settings locally

## Save System
- Data is saved using PlayerPrefs
- All PlayerPrefs keys are stored inside:
  - `Keys` class

## Data Management
`DataManager` handles:
- Total coins
- Turret shop unlocks
- Turret stock data
- Level index save
- Level progress save

## Build System
`BuildManager` handles:
- Turret placement
- Build logic
- Placement validation

> Note:
> Tower placement currently works using touch input, so placement logic is mainly designed for mobile/simulator testing.

## Level System
- Levels are created using Scriptable Objects
- Demo levels are available inside:
  - `Assets/AllLevels`
- `GameManager` stores and loads levels from:
  - `AllLevels` array

## Node System
`Node` script handles:
- Tile input
- Turret placement logic
- Placement control on tiles

## Enemy System
Enemy script handles:
- Health
- Speed
- Damage rate
- Coin reward
- Health bar UI

### Enemy Types
- Enemy_Type_1
- Enemy_Type_2
- Enemy_Type_3

Each enemy has different:
- Speed
- Damage
- Coin value

## Turret System
The project includes multiple turret types with different stats and behaviours.

---

# Controls

## PC Controls
| Action | Input |
|---|---|
| Place Turret | Touch | Simulator
| Select UI | Touch |
| Game Interaction | Touch |

## Mobile Controls
| Action | Input |
|---|---|
| Place Turret | Touch |
| Select UI | Tap |

---

# Setup Instructions

## Unity Version
Recommended Unity Version:
- Unity 6 or newer

## How To Open
1. Open Unity Hub
2. Click Open Project
3. Select Tower_Defense_Repo/TowerDefence3DMain Folder
4. Wait for packages and assets to load
5. If Any Error Happen because of unity UI or Like that please Go Packagemanger>Install>2D(com.unity.feature.2d)
6. Open Assets/Scenes -> Open Gameplayscene (other scene are made for taking screenshot for making Turnet Icons)
+
## Play The Game
1. Open the main scene
2. Press Play inside Unity Editor
3. Make Sure Play in Simulator Because Placement work on touch.

---

# Demo Levels

You can find example levels inside:

```text
Assets/AllLevels