# 🎮 3D Bouncing Ball Game

[![Unity](https://img.shields.io/badge/Unity-2021.3+-000000?style=flat&logo=unity)](https://unity.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Mac%20%7C%20Linux-lightgrey)](https://github.com/daneyyhh/3d-bouncing-ball-game)

A professional 3D bouncing ball game with realistic physics, health system (spikes/hearts), jumpable obstacles, and water mechanics. Built with Unity and designed for immersive gameplay with polished visuals and smooth controls.

![Game Banner](https://via.placeholder.com/800x400/0066CC/FFFFFF?text=3D+Bouncing+Ball+Game)

## ✨ Features

### Core Gameplay
- 🎯 **Realistic Physics**: Bouncy ball with Unity's physics engine for natural movement
- 🕹️ **Smooth Controls**: WASD/Arrow keys for movement + Space for super jump
- 🎨 **Professional Graphics**: PBR materials, post-processing effects, dynamic lighting
- 🏃 **Automatic Bouncing**: Ball bounces continuously on surfaces

### Game Mechanics
- ❤️ **Health System**: Track your health with visual UI elements
- 🔺 **Spikes**: Dangerous obstacles that decrease health on contact
- 💚 **Heart Pickups**: Collectibles that restore your health
- 🧱 **Obstacles**: Various platforms and blocks to jump over
- 🌊 **Water Physics**: Realistic floating mechanics in water areas
- 🎯 **Collision Detection**: Precise physics-based interactions

### Polish & Effects
- ✨ Particle effects for impacts and pickups
- 🔊 Sound effects for actions (bounce, damage, heal, water)
- 📱 Responsive camera follow system
- 🎮 Optimized performance for smooth 60+ FPS

## 🚀 Getting Started

### Prerequisites
- Unity 2021.3 or newer
- Basic knowledge of Unity Editor
- Windows, macOS, or Linux

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/daneyyhh/3d-bouncing-ball-game.git
   cd 3d-bouncing-ball-game
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and select the cloned project folder
   - Open the project (Unity will import all assets)

3. **Open the Main Scene**
   - Navigate to `Assets/Scenes/`
   - Double-click `MainScene.unity`

4. **Play the Game**
   - Press the Play button in Unity Editor
   - Or build the game: File → Build Settings → Build

## 🎮 Controls

| Key | Action |
|-----|--------|
| **W / ↑** | Move Forward |
| **A / ←** | Move Left |
| **S / ↓** | Move Backward |
| **D / →** | Move Right |
| **Space** | Super Jump |
| **Esc** | Pause Menu |

## 📁 Project Structure

```
3d-bouncing-ball-game/
├── Assets/
│   ├── Scripts/
│   │   ├── BallController.cs      # Main player controller
│   │   ├── HealthManager.cs       # Health system logic
│   │   ├── PickupHeart.cs         # Heart pickup script
│   │   ├── SpikeHazard.cs         # Spike damage script
│   │   ├── WaterZone.cs           # Water physics script
│   │   └── CameraFollow.cs        # Camera controller
│   ├── Materials/
│   │   ├── BallMaterial.mat       # Ball PBR material
│   │   ├── SpikeMaterial.mat      # Spike material
│   │   └── WaterMaterial.mat      # Water shader material
│   ├── Prefabs/
│   │   ├── Ball.prefab            # Player ball prefab
│   │   ├── Spike.prefab           # Spike hazard prefab
│   │   ├── Heart.prefab           # Health pickup prefab
│   │   └── Obstacle.prefab        # Platform/obstacle prefab
│   ├── Scenes/
│   │   └── MainScene.unity        # Main game scene
│   ├── Audio/
│   │   ├── Bounce.wav
│   │   ├── Damage.wav
│   │   ├── Heal.wav
│   │   └── Water.wav
│   └── UI/
│       └── HealthUI.prefab        # Health display UI
├── ProjectSettings/
└── README.md
```

## 🛠️ Core Scripts

### BallController.cs
Main player controller handling:
- Movement with Rigidbody physics
- Automatic bouncing
- Super jump mechanic
- Water floating
- Collision detection

### HealthManager.cs
Manages player health:
- Current health tracking
- Max health limit
- Damage and healing methods
- UI updates
- Game over detection

### SpikeHazard.cs
Spike obstacle behavior:
- Collision detection with ball
- Damage application
- Visual feedback
- Sound effects

### PickupHeart.cs
Heart collectible:
- Trigger detection
- Health restoration
- Rotation animation
- Pickup effects

### WaterZone.cs
Water physics zone:
- Buoyancy force application
- Entry/exit detection
- Visual effects (splashes)
- Drag simulation

## 🎨 Customization

### Adjusting Game Parameters

Edit values in Unity Inspector:

**BallController**
- `moveSpeed`: Movement speed (default: 5)
- `jumpForce`: Jump power (default: 8)
- `bounceForce`: Auto-bounce strength (default: 5)
- `waterFloatForce`: Water buoyancy (default: 7)
- `maxHealth`: Starting health (default: 3)

**Materials**
- Modify PBR properties for different looks
- Add custom textures and normals
- Adjust metallic/smoothness values

**Level Design**
- Duplicate and arrange obstacle prefabs
- Position spikes and hearts strategically
- Create water zones with box colliders (trigger)
- Design challenging platforming sections

## 🔧 Advanced Features

### Adding New Obstacles

1. Create a 3D model in Unity
2. Add appropriate collider
3. Tag it as "Obstacle"
4. Add custom behavior scripts
5. Save as prefab

### Custom Power-Ups

```csharp
public class SpeedBoost : MonoBehaviour
{
    public float boostMultiplier = 2f;
    public float duration = 5f;
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            BallController ball = other.GetComponent<BallController>();
            StartCoroutine(ApplyBoost(ball));
            Destroy(gameObject);
        }
    }
    
    IEnumerator ApplyBoost(BallController ball) {
        float originalSpeed = ball.moveSpeed;
        ball.moveSpeed *= boostMultiplier;
        yield return new WaitForSeconds(duration);
        ball.moveSpeed = originalSpeed;
    }
}
```

### Multiple Levels

1. Create new scenes in `Assets/Scenes/`
2. Design unique level layouts
3. Add scene management script
4. Implement level progression system
5. Save player progress with PlayerPrefs

## 🎯 Gameplay Tips

- 💡 Use momentum to reach high platforms
- 🛡️ Avoid spike clusters - they're instant danger
- 💚 Collect hearts before difficult sections
- 🌊 Use water to reach floating platforms
- 🏃 Time your jumps precisely on moving obstacles

## 📊 Performance Optimization

- Object pooling for pickups and effects
- LOD (Level of Detail) for complex models
- Occlusion culling for large levels
- Baked lighting for static objects
- Compressed audio formats
- Texture atlasing for materials

## 🐛 Troubleshooting

### Common Issues

**Ball falls through floor**
- Increase physics timestep: Edit → Project Settings → Time
- Check collider settings on floor
- Ensure Rigidbody collision detection is set to "Continuous"

**Controls not working**
- Verify Input Manager settings: Edit → Project Settings → Input Manager
- Check if ball has BallController script attached
- Ensure Rigidbody component is present

**Performance issues**
- Reduce post-processing quality
- Lower shadow resolution
- Disable vsync if not needed
- Use profiler to identify bottlenecks

**Water not floating**
- Check WaterZone trigger collider
- Verify "Water" tag is assigned
- Increase waterFloatForce value

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**daneyyhh**
- GitHub: [@daneyyhh](https://github.com/daneyyhh)

## 🙏 Acknowledgments

- Unity Technologies for the game engine
- Unity Asset Store for inspiration
- Physics simulation based on Unity's PhysX
- Community tutorials and documentation

## 📧 Support

If you have questions or need help:
- Open an issue on GitHub
- Check existing issues for solutions
- Review Unity documentation

## 🗺️ Roadmap

- [ ] Add more obstacle types (moving platforms, rotating hazards)
- [ ] Implement multiple levels with progression
- [ ] Add checkpoint system
- [ ] Create level editor mode
- [ ] Add multiplayer support
- [ ] Implement leaderboards
- [ ] Mobile platform support (touch controls)
- [ ] Add more power-ups (shield, speed boost, double jump)
- [ ] Create tutorial level
- [ ] Add visual themes/skins

## 🎬 Screenshots

*Coming soon! Add screenshots of your game here.*

---

⭐ If you like this project, please give it a star!

Made with ❤️ using Unity
