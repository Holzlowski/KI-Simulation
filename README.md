# KI-Simulation
This project was developed as part of my **Bachelor thesis** and focuses on implementing and analyzing **AI behavior systems** for animals within a Unity environment. The goal was to explore how game AI can simulate lifelike decision-making based on internal states and environmental perception.

## 🎯 Project Focus

Each animal agent in the simulation makes decisions based on **dynamic internal values** such as:

- 🥩 **Hunger**
- ❤️ **Health**
- 👀 **Perception of surroundings** (e.g., presence of food, other animals, threats)

The agents evaluate their current needs and surroundings, then choose behaviors accordingly — for example:  
- Searching for food when hungry  
- Avoiding threats when in low health  
- Roaming or resting when no urgent need is present

## 🧠 AI Techniques Explored

- **State-based behavior logic**  
  Animals transition between behavioral states (e.g., idle, seek, flee) based on thresholds and context.

- **Perception systems**  
  Agents react to nearby objects based on line of sight, distance, and object type.

- **Priority-driven decision-making**  
  Needs are evaluated dynamically and influence which behavior is selected at any given moment.

## 🛠️ Technologies Used

- Unity Engine
- C#
- NavMesh for pathfinding
- Custom state machines and perception systems

## 🎓 Purpose

This project was created to deepen my understanding of **artificial intelligence in video games**, with a particular focus on:
- Designing flexible and believable AI behavior
- Managing competing needs and dynamic priorities
- Creating immersive, responsive simulations

---

This simulation provides a foundational system that could be extended to larger ecosystems, predator-prey mechanics, or more complex social behavior models.
