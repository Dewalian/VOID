# VOID

![VOID](https://github.com/user-attachments/assets/78fe2a2d-7963-4bfc-b5ab-ab75654c1029)

  In a vast void space, you try to survive hordes of aliens' with your spaceship. Each time you kill them, they get stronger and faster. You knew you never had the chance, but you kept going. How many waves can you survive?


## About Game

  VOID is a retro topdown endless shotter where you try to survive a bunch of aliens. The game gets more difficult on each level. Get to the next level by killing a wave of enemies.

## Scripts and Features
| Script | Description |
| --- | --- |
| `GameManager.cs` | Manages the game's start and end, as well as the difficulty increase. |
| `Tank.cs` | Handles the control of the tank: moving, shooting, and taking damage. |
| `Enemy.cs` | Handles the AI of enemy, mostly on following the tank's movement. |
| `EnemySpawner.cs` | Controls where the enemy should spawn. |
| `etc` | |


## Mechanics
### Health Point
You control a ship where it have a set amount of health. Collide with an enemy will result in loss of 1 HP. 

![VOIDhealthgif](https://github.com/user-attachments/assets/66437e32-a45e-44ca-a8e0-a42cc01a75ac)

You get an additional HP each time you clear a level.
The game ends when you your HP equals zero.

### Level
Each level makes the game more difficult.

![VOID level comparison](https://github.com/user-attachments/assets/717be448-b64a-4ed6-a262-7cdda948ef20)

Get to the next level by kill a specific amount of enemy, indicated by the number on the middle where it increases with each level (enemyKillQuota).
On level up, InreaseDifficulty function is called, where the enemy spawn rate is shorter and enemy speed is faster.
But, your tank also gain a buff, where its shoot CD is reduced, movement speed is faster, and HP is increased.

### Anti spawn zone
The tank has an invisible zone where enemies can't spawn, so the enemies can't immediately collide with player upon spawn.

![image](https://github.com/user-attachments/assets/5b835490-3ff1-47d7-bf55-2ec17fac952d)

<a href="https://jeje8.itch.io/void">Play it here</a>





