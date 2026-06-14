# 3D Survival Shooter Game

A top-down 3D survival shooter built in Unity where you fight off waves of enemies for as long as possible while managing your health and using power-ups to stay alive.

## Built With

* [![Unity][Unity.com]][Unity-url]

[Unity.com]: https://img.shields.io/badge/Unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black
[Unity-url]: https://unity.com/

---

## How to Play

### Objective
Survive as long as possible against endless waves of enemies. Every enemy you kill earns you points. The game ends when your health reaches zero.

### Movement
Use the **W / A / S / D** keys or the **Arrow Keys** to move your character in any direction. Your character always faces toward your mouse cursor, allowing you to move in one direction while aiming in another — a key skill for dodging enemies while keeping your shots on target.

### Aiming & Shooting
Move your **mouse** to aim. Your character automatically rotates to face wherever your cursor points on the floor. Hold down the **Left Mouse Button** to shoot continuously. The gun fires rapid shots in a straight line toward your aim point, so keep your cursor on the enemy to deal damage effectively. Each shot deals 20 damage to an enemy.

### Health
Your health starts at **100** and is displayed as a slider on the screen. Every time an enemy reaches and touches you, it deals **10 damage** every 0.5 seconds. When your health drops to zero, the game is over and a Game Over screen appears. You can then restart from the beginning.

### Enemies
Three types of enemies spawn periodically and relentlessly chase you across the arena using pathfinding navigation:
- **Zombunny** — a fast, small zombie rabbit that closes the distance quickly.
- **ZomBear** — a larger, more threatening zombie bear.
- **Hellephant** — a powerful hellish elephant that hits hard.

Each enemy starts with **100 health**. Shoot them repeatedly to whittle their health down. Once defeated, they play a death animation and sink into the ground. Enemies continue spawning at regular intervals, so the pressure never lets up.

### Power-Ups
Two types of power-ups can appear in the arena. Walk over them to collect them instantly:
- **Healing (Heart icon)** — Restores **40 health** points. If your health is already above 60, it caps at the maximum of 100. Prioritize picking this up whenever you are low on health.
- **Speed Up (Star icon)** — Boosts your movement speed from the default **6** up to **10** for **10 seconds**, making it much easier to outrun and reposition away from enemies. After the duration expires, your speed returns to normal automatically.

### Score
Your score is displayed in the top corner of the screen and increases each time you kill an enemy. Each enemy kill is worth **10 points**. Try to beat your personal best by surviving longer and taking down more enemies.

### Pausing the Game
Press the **Pause button** in the UI to pause the game at any time. The game world freezes completely and all audio stops. Press it again to resume.

---

## Features
1. **Power-Ups** — Speed boost and healing pickups to aid survival
2. **HUD** — Live score counter, health bar, and pause functionality
3. **Enemies** — Three distinct enemy types with AI pathfinding and attack behavior
4. **Sound Effects** — Audio feedback for shooting, taking damage, enemy deaths, and power-up collection

---

## Prerequisite

- [Unity 2018.3.3](https://unity3d.com/get-unity/download/archive)

## Installation

1. Install [Unity](https://store.unity.com/front-page?check_logged_in=1#plans-individual) on your machine
2. Open Unity Hub and click **Add project from disk**
3. Select this project folder and open it
4. Once loaded, open the `Level_01` scene from `Assets/Scenes/` and press **Play**

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request.

## License

All the code available under the MIT license. See [LICENSE](LICENSE).
