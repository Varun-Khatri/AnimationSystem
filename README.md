# 2D Animation System

A flexible, state-based 2D animation system for Unity with parameter-driven transitions and frame events. Perfect for character animations, UI effects, and sprite-based games.

## 📦 Installation & Setup

### Package Structure

```text
Assets/Packages/[Package Name]/
├── Runtime/                 # Core system files
│   ├── [MainSystemFiles].cs
│   └── ...
└── Samples/                 # Sample implementations
    ├── ExampleComponent1.cs
    ├── ExampleComponent2.cs
    └── ExampleScene.unity   (if included)
```

### Installation Methods
**Method 1: Unity Package Manager (Recommended)**

- Open Window → Package Manager
- Click + → Add package from git URL
- Enter your repository URL:

```text
https://github.com/[username]/[repository-name].git
The system will be installed in Assets/Packages/[System Name]/
```

**Method 2: Manual Installation**

- Download the repository or clone it
- Copy the entire package folder to:

```text
Assets/Packages/[System Name]/
The system is ready to use
```

### Accessing Samples

After installation, access samples at Assets/Packages/[System Name]/Samples/

## 🎯 Features

- **State Machine Architecture** - Organize animations into logical states
- **Parameter-Driven Transitions** - Control transitions with bool and trigger parameters
- **Frame Events** - Execute logic at specific animation frames
- **ScriptableObject Based** - Easy to create and manage animations in the editor
- **Lightweight & Performant** - Optimized for 2D games
- **Extensible Design** - Easy to add new parameter types and features

## 🏗️ Architecture

### Core Components

| Component | Description |
|-----------|-------------|
| `AnimationController` | Main controller that plays animations and manages states |
| `AnimationClip` | Container for sprite frames, timing, and events |
| `AnimationState` | Represents a single animation state with transitions |
| `AnimationParameter` | Base class for animation parameters (Bool, Trigger) |
| `AnimationTransition` | Defines transitions between states based on parameters |

## 🚀 Quick Start

### 1. Create Animation Clips

In the Unity Editor:
- Right-click in Project → Create → Animation System → Animation Clip
- Configure frames, frame rate, and loop settings

### 2. Set Up Animation States

**Create AnimationState Assets:**
- Right-click in Project → Create → C# Script → Name it "AnimationState"
- Create multiple states for your character (Idle, Run, Jump, etc.)

**Configure Each State:**
```csharp
// In the Inspector for each AnimationState:
// - State Name: "Idle", "Run", "Jump", etc.
// - Clip: Assign the corresponding AnimationClip
// - Transitions: Add transitions to other states
```

### 3. Create Parameters
**Bool Parameters (for persistent states):**

Right-click → Create → Animation System → Parameters → Bool

Names: "IsRunning", "IsGrounded", "IsAttacking"

**Trigger Parameters (for one-time events):**

Right-click → Create → Animation System → Parameters → Trigger

Names: "Jump", "Attack", "TakeDamage"

### 4. Configure State Transitions
For each AnimationState, set up transitions:

**Idle State Example:**

```csharp
// Add Transition 1:
// - Parameter: "IsRunning" (Bool)
// - Target State: RunState
// - Bool Condition: true

// Add Transition 2:
// - Parameter: "Jump" (Trigger) 
// - Target State: JumpState
// - Bool Condition: true (triggers always when parameter is set)
```
**Run State Example:**

```csharp
// Add Transition 1:
// - Parameter: "IsRunning" (Bool)
// - Target State: IdleState  
// - Bool Condition: false

// Add Transition 2:
// - Parameter: "Jump" (Trigger)
// - Target State: JumpState
// - Bool Condition: true
```

### 5. Set Up the Animation Controller

On your GameObject:

- Add AnimationController component
- Drag your AnimationState assets into the States array
- Set the Default State to your initial state (usually "Idle")
  
### 6. Basic Implementation

```csharp
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private AnimationController _animController;
    
    private void Update()
    {
        // Control animations with parameters
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animController.SetParameter("Jump", true);
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animController.SetParameter("IsRunning", true);
        }
        else
        {
            _animController.SetParameter("IsRunning", false);
        }
    }
}
```

### 7. Add Frame Events (Optional)

In your AnimationClip:

- Select the clip in Project view
- In Inspector, add events to specific frames
- Frame 0: Play footstep sound
- Frame 3: Enable attack hitbox
- Frame 6: Disable attack hitbox
  
## 📖 API Reference

### AnimationController
| Method| 	Description| 
|-----------|-------------|
| SetParameter<T>(string name, T value) | 	Sets animation parameters (bool/trigger)| 
| PlayState(AnimationState state | )	Plays a specific animation state| 

### Animation Events
| Event Type	| Usage| 
|-----------|-------------|
| AnimationEvent | 	Execute UnityEvents at specific frames| 
| BoolParameter | 	Persistent boolean conditions| 
| TriggerParameter | 	One-time transition triggers| 

## 🔧 Configuration

### 1. Create Animation Clips

- Create → Animation System → Animation Clip
- Add sprite frames in order
- Set frame rate (typically 12-24 for smooth animation)
- Enable looping for idle/run animations
- Add frame events for specific actions

### 2. Set Up Parameters

- Create → Animation System → Parameters → Bool
- Create parameters like "IsRunning", "IsGrounded", "IsAttacking"
- Create → Animation System → Parameters → Trigger
- Create triggers like "Jump", "Attack", "TakeDamage"

### 3. Configure Animation States
   
- Create AnimationState assets and set up transitions:

```csharp
// Example state structure:
// - Idle State
//   → Transition to Run State when "IsRunning" = true
//   → Transition to Jump State when "Jump" trigger = true

// - Run State  
//   → Transition to Idle when "IsRunning" = false
//   → Transition to Jump when "Jump" trigger = true
```

## 💡 Usage Examples

### Character Animation Controller
```csharp
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private AnimationController _animController;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GroundChecker _groundChecker;
    
    private void Update()
    {
        // Set movement parameters
        bool isMoving = Mathf.Abs(_rb.velocity.x) > 0.1f;
        _animController.SetParameter("IsMoving", isMoving);
        
        // Set ground parameter
        _animController.SetParameter("IsGrounded", _groundChecker.IsGrounded);
        
        // Set facing direction
        if (Mathf.Abs(_rb.velocity.x) > 0.1f)
        {
            _animController.SetParameter("FacingRight", _rb.velocity.x > 0);
        }
    }
    
    public void OnJump()
    {
        _animController.SetParameter("Jump", true);
    }
    
    public void OnAttack()
    {
        _animController.SetParameter("Attack", true);
    }
    
    public void OnTakeDamage()
    {
        _animController.SetParameter("TakeDamage", true);
    }
}
```

### Frame Event Example
```csharp
// Configure frame events in the AnimationClip inspector
// Frame 0: Play footstep sound
// Frame 3: Enable attack hitbox
// Frame 6: Disable attack hitbox
// Frame 10: Reset attack state
```

### Enemy Animation Setup
```csharp
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private AnimationController _animController;
    
    public void PlayIdle() => _animController.SetParameter("IsIdle", true);
    public void PlayChase() => _animController.SetParameter("IsChasing", true);
    public void PlayAttack() => _animController.SetParameter("Attack", true);
    public void PlayDeath() => _animController.SetParameter("IsDead", true);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animController.SetParameter("IsChasing", true);
        }
    }
}
```

## 🤔 Why This System?

### When to Use This Over Unity's Animator

| Scenario |	This System |	Unity Animator |
|----------|--------------|----------------|
| Simple 2D Games	| ✅ Perfect fit	| ❌ Overkill | 
| Mobile Performance | 	✅ Lightweight	| ❌ Higher overhead | 
| Programmer-Centric	| ✅ Code-first	| ❌ Designer-focused |
| Version Control	| ✅ Text-friendly	| ❌ Binary files |
| Rapid Prototyping	| ✅ Quick setup	| ❌ Complex setup |

Bottom Line: This system is perfect for simple 2D games where Unity's Animator would be overkill. It demonstrates clean architecture and understanding of animation principles while being lightweight and programmer-friendly.

## 🛡️ Best Practices
- Organize Your States
- Use Appropriate Parameter Types
- Optimize Frame Rates

## 🎯 Use Cases

- 2D Platformers - Character movement and actions
- RPG Characters - Combat, spells, and interactions
- Enemy Behaviors - Patrol, chase, attack states
- UI Animations - Menu transitions and feedback
- Environmental Effects - Animated props and objects

## 🤝 Contributing

This system is part of my professional portfolio. Feel free to:

- Use in your personal or commercial projects
- Extend with new parameter types (Float, Integer)
- Add blend trees or layered animations
- Adapt for your specific animation needs


