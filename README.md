# System

**System** is a dual-layer Unity game where hacking the world means physically breaking into it.

## Concept

-A cyber-anti-system hacker takes on corrupt governments — but to hack anything, you first have to reach a physical terminal in the real world.

The game alternates between two layers:

- **Open-world layer (3D):** parkour/stealth through a physical city to reach hackable terminals.
- **Cyberspace layer (Doom-style FPS):** once connected to a terminal, you become a virus inside the system — a 2D billboard-sprite, wireframe-neon retro-FPS level where you fight the system's defenses from within.

## Current State

- Playable technical demo of the FPS/cyberspace layer (no textures/sound), ~10 min level with an ending.
- Core systems implemented: player movement & gravity, enemy AI (melee + ranged, billboard sprites), weapon system (shotgun, laser gun, pickups from floor), health/ammo UI, key-gated doors, first map.
- Open-world/parkour layer: not yet implemented — currently the project's main missing piece.

## Status
- Development is currently paused (since ~September) due to workload from my CS degree and another project (MURGILDU, Godot XR) but is planned to resume in the near future.
 
## Tech Stack

- **Engine:** Unity
- **Version control history:** originally tracked in Unity Version Control (Plastic SCM); migrated to Git preserving original commit dates (see commit history).

## Roadmap / Next Steps

- [ ] Textures, sound, and visual polish for the FPS layer
- [ ] Increase the amount of playable maps for the FPS layer
- [ ] Open-world layer: parkour movement + terminal-hacking trigger
- [ ] Connect open-world and cyberspace layers into one loop
- [ ] More enemy types / weapon variety

## Development Notes

This project started as a solo prototype to test the dual-layer concept. Development was irregular (~3 months of real work spread over a longer calendar period). Development history was migrated from Unity Version Control to Git; commit dates reflect the original UVC changeset timestamps, not the upload date.
