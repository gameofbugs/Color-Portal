# 🌀 Color Portal

> **Explore a colorless world, pass through mystical portals, and collect chromatic crystals to restore color to a dying world.**

![Platform](https://img.shields.io/badge/Platform-PC-blue?style=flat)
![Engine](https://img.shields.io/badge/Engine-Unity-000000?style=flat&logo=unity&logoColor=white)
![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat&logo=csharp&logoColor=white)
![Status](https://img.shields.io/badge/Status-Game%20Jam%20Build-orange?style=flat)
![Theme](https://img.shields.io/badge/Jam%20Theme-Portal%20Pass-purple?style=flat)

---

## 🎮 Play It

[![Play on itch.io](https://img.shields.io/badge/Play_on-itch.io-FA5C5C?style=flat&logo=itch.io&logoColor=white)](https://gameofbugsofficial.itch.io/color-portal)

---

## 📖 About

Color Portal was built for a Game Jam under the theme **"Portal Pass."**

The world has been drained of all color. As the player, you explore a stark monochromatic overworld, discover hidden portals, and travel to vibrant color dimensions — each guarding a chromatic crystal. Collect all crystals to summon the Color Spirit and restore the world.

---

## 🕹️ Gameplay

**Core Loop:**
1. Explore the monochromatic overworld
2. Find and activate color portals (Red, Blue, Yellow worlds)
3. Navigate color-themed dimensions — volcanic, underwater, electric
4. Defeat the guardian or solve the puzzle to claim the crystal
5. Return to the overworld — watch it gradually regain color
6. Collect all crystals → summon the Color Spirit → world restored

**Controls:**

| Input | Action |
|-------|--------|
| WASD / Arrow Keys | Move |
| Spacebar | Jump |
| E | Activate portal / Interact |
| Mouse Click / Z | Attack |

---

## 🔧 What I Built

- **Portal Teleportation System** — trigger-based dimensional travel with screen transition effects and two-way portal travel
- **Color State Manager** — tracks which crystals are collected and dynamically updates overworld materials and shaders
- **Shader-Based Color Restoration** — overworld gradually fills with color as each crystal is collected, using material property blocks
- **Enemy AI & Guardian System** — each color world has a themed guardian with simple patrol and attack behavior
- **UI & HUD** — crystal counter, health bar, minimap with portal markers, interaction prompts

---

## 🌍 Color Worlds

| World | Theme | Hazards | Guardian |
|-------|-------|---------|----------|
| 🔴 Ember Realm | Volcanic / Lava | Fire geysers, lava pits | Fire Elemental |
| 🔵 Aqua Depths | Underwater / Ice | Currents, air bubbles | Water Serpent |
| 🟡 Radiant Plains | Electric / Light | Lightning strikes, energy barriers | Thunder Beast |

---

## 🧠 What I Learned

- Implementing **portal teleportation** with scene-persistent state in Unity without losing game data between dimension loads
- Using **shader material properties** at runtime to drive color restoration visuals dynamically based on game state
- Structuring a **GameManager singleton** to cleanly track crystal collection state across scenes
- Scoping a full game loop — narrative, levels, UI, audio, and build — within a tight jam deadline

---

## 🗂️ Project Structure

```
Assets/
├── Scripts/
│   ├── GameManager.cs          # Crystal state, game flow
│   ├── PlayerController.cs     # Movement, jump, dash, attack
│   ├── PortalController.cs     # Teleportation and transition effects
│   ├── ColorStateManager.cs    # Tracks collected colors, updates world materials
│   ├── EnemyAI.cs              # Guardian patrol and attack logic
│   └── UIManager.cs            # HUD, crystal counter, minimap
├── Scenes/
│   ├── Overworld.unity
│   ├── RedWorld.unity
│   ├── BlueWorld.unity
│   └── YellowWorld.unity
├── Shaders/                    # Color restoration shader graphs
├── Prefabs/                    # Portal gates, crystals, enemies
└── Audio/                      # Themed music per world + SFX
```

---

## ⚙️ Tech

| Tool | Use |
|------|-----|
| Unity | Game engine |
| C# | All game logic and systems |
| Shader Graph | Runtime color restoration visuals |
| Visual Studio | IDE |

---

## 🚀 Run Locally

1. Clone: `git clone https://github.com/gameofbugs/Color-Portal`
2. Open in Unity Hub
3. Open `Overworld` scene and press Play

---

## 👤 About

Built solo by **Manoj S** for a Game Jam — one of 7 shipped Unity games.
More projects: [gameofbugs.official.itch.io](https://gameofbugsofficial.itch.io/color-portal)
