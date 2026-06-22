
# 3D Survival Shooter Game

A top-down 3D survival shooter built in Unity. You fight endless enemy waves, manage your health, collect power-ups, and try to survive as long as possible while building your score.

## What The Game Does

This project has three main scenes:

1. `MainMenuGScene` is the entry point. It shows the main window panel and the leaderboard panel.
2. `Level_01` is the gameplay scene where you fight enemies, earn score, and lose health.
3. `EndGameScene` is the results screen that appears when the player dies.

When the player dies, the game loads `EndGameScene` directly to display:
- Final score
- Number of enemies killed
- Time survived
- Replay, back to main menu, and quit buttons

---

## How To Play

### Objective
Survive as long as possible against endless waves of enemies. Every enemy kill increases your score and kill count. The run ends when your health reaches zero.

### Movement
Use **W / A / S / D** or the **Arrow Keys** to move your character in any direction. Your character always faces toward your mouse cursor, allowing you to move in one direction while aiming in another.

### Aiming & Shooting
Move your **mouse** to aim. Your character automatically rotates to face wherever your cursor points on the floor. Hold the **Left Mouse Button** to shoot continuously. Each shot deals damage to enemies, and killing an enemy increases your score by **10 points**.

### Health
Your health starts at **100** and is displayed as a slider on the HUD. Every time an enemy reaches and touches you, it deals **10 damage** every 0.5 seconds. The slider decreases as you take damage, and when health reaches zero the game loads the end-game scene.

### Enemies
Three enemy types spawn periodically and relentlessly chase you using pathfinding navigation:
- **Zombunny** — a fast, small zombie rabbit
- **ZomBear** — a larger, threatening zombie bear
- **Hellephant** — a powerful hellish elephant

Each enemy starts with **100 health**. Shoot them repeatedly to defeat them. Once defeated, they play a death animation and disappear. Enemies continue spawning at regular intervals, so the pressure never lets up.

### Power-Ups
Two types of power-ups can appear in the arena. Walk over them to collect them instantly:
- **Healing (Heart icon)** — Restores **40 health** points. Caps at maximum of **100**.
- **Speed Up (Star icon)** — Boosts your movement speed from **6** up to **10** for **10 seconds**, making it easier to outrun and reposition away from enemies.

### Score And Time
The score counter updates during gameplay, the kill count tracks how many enemies you defeated, and the timer tracks how long the run lasted. Those values are saved and shown again on the end-game scene.

### Pausing
Press the **Pause button** in the UI to pause and resume the game at any time.

---

## Menu And End-Game Flow

### Main Menu
In `MainMenuGScene`, the main panel is the default view. From there you can:
1. Start a new game
2. Continue a saved run if one exists
3. Open the leaderboard
4. Open the difficulty panel
5. Quit the game

### Leaderboard Panel
The leaderboard panel has a back button that returns you to the previous main window panel instead of leaving the menu scene.

### End-Game Scene
When the player dies, the game switches to `EndGameScene` to display the saved results and offer:
1. Replay, which starts `Level_01` again
2. Back to main menu, which returns to `MainMenuGScene`
3. Quit, which exits the application

---

## Features

1. Live score, kill count, and survival time tracking
2. Separate end-game results scene
3. Leaderboard panel with saved sessions
4. Health slider and damage feedback
5. Enemy pathfinding and attacks
6. Power-ups for healing and speed boost
7. Replay, back, and quit actions from the end-game screen
8. Pause functionality

---

## Prerequisite

* Unity 6000.3.16f or newer

## Installation

1. Install Unity on your machine.
2. Open Unity Hub and choose **Add project from disk**.
3. Select this project folder.
4. Make sure `MainMenuGScene`, `Level_01`, and `EndGameScene` are included in Build Settings.
5. Press Play from `Level_01` to test the gameplay flow.

## How The Implementation Works

1. Enemies add score and increment the kill count when they die.
2. The player health script updates the slider every time damage is taken.
3. The game-over manager saves the run data and loads `EndGameScene`.
4. The end-game scene controller reads the saved data and fills in the results text.
5. The replay, back, and quit buttons are wired directly to the end-game scene controller.
6. The leaderboard back button returns to the main menu panel without leaving the menu scene.

## Contributing

Contributions are welcome. If you want to improve the game, fork the repo and open a pull request.

