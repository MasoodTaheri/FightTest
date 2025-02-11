# FightTest

# Character Switching & AI System for Unity (Realtime Game)

This project implements a **real-time character switching system** in Unity, allowing players to dynamically switch between multiple characters. Each character is equipped with **basic movement**, ensuring fluid control and smooth transitions. Since graphics were not a focus, the project was built using **free or no assets**, with Animancer (Free) handling animations.

## 🔹 Features

✅ **Realtime Character Switching** – Instantly swap between different characters during gameplay.  
✅ **Smooth Character Movement** – Implemented for basic navigation and playability.  
✅ **Smart AI Behavior** – AI-controlled characters utilize a **state machine** to:  
- Move dynamically across the map.  
- Attack, strafe, and evade enemies instead of following a predictable pattern.  
- Make combat feel natural and responsive.  
✅ **Health-Based Switching** – If an AI-controlled character's health is low, it will automatically switch to another available character with sufficient health.  

## 🔹 Architecture & Design

- **MVP Pattern for UI** – Ensures modularity, maintainability, and separation of concerns.  
- **State Machine for AI** – Provides structured, scalable AI behavior with clear transitions.  
- **Animancer (Free Version) for Animation** – Handles animation playback without requiring Animator Controllers.  
- **SOLID, KISS & DRY Principles** – Code is clean, efficient, and easy to extend.  

## 🔹 About

This project is designed as a **gameplay-focused system**, emphasizing mechanics over visuals. It is ideal for learning and testing **real-time character management, AI behavior, and structured game architecture** in Unity.  

---

Feel free to contribute or provide feedback! 🚀  
