# Kitchen Smash
Built while following Code Monkey's Overcooked-style Unity tutorial. At some point I stopped just following along and started making it my own adding systems, refactoring things I wasn't happy with, and figuring out why stuff actually works rather than just copying it.

This project could realistically be finished in 1-2 weeks, but I took longer because I didn't follow the tutorial's code structure I wrote and structured everything myself as a personal challenge. No regrets, learned way more that way.

---

## What I changed

Stove counter remembers cook progress per item pick it up mid cook, put it back, it resumes from where it left off

Built a ScriptableObject based recipe and ingredient system that's easy to extend without touching code

Order tracking UI uses a `List<(_Recipe, Transform)>` tuple to map each recipe to its card, so duplicate orders (two burgers) are handled independently

Improved UI with animated order cards, stove warning states, and a progress bar that shows both cook and burn phase in one continuous fill

Improved visuals with post processing, stylized skybox, and street environment outside the kitchen

<img width="1280" height="719" alt="photo_2026-05-16_14-16-33" src="https://github.com/user-attachments/assets/ba4032f7-bb49-4fa2-8994-d884eb66c649" />
<img width="1280" height="719" alt="photo_2026-05-16_14-16-29" src="https://github.com/user-attachments/assets/cd46f925-4114-4178-9e34-9dfffae163ee" />

---

## What I learned

**New Input System** — how action maps, bindings, and `performed` callbacks work. Also how to do interactive rebinding at runtime and save the override as JSON to PlayerPrefs.

**ScriptableObjects** — using them as data containers and as keys for recipe lookups. Understanding why comparing SO references directly is cleaner than string matching.

**Interfaces and Enums** — `ICounter` as a shared contract across `ClearCounter`, `CuttingCounter`, `StoveCounter`, `PlateCounter` and so on. Each implements the same methods differently. Enums for type safe state like `CookState` and `GameState` instead of magic strings or booleans.

**EventArgs and C# Events** — `EventHandler<OrderData>` for passing recipe data between the delivery counter and the UI. `Action` for lighter one way signals like cook state changes. Understanding the difference between `Action`, `EventHandler`, and when to use each.

---



## Download
 
[![Download for Windows](https://img.shields.io/badge/Download-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/EntVexlon/Project-KitchenSmash/releases)

---
## Credit

Built following the tutorial by [Code Monkey](https://www.youtube.com/@TheCodeMonkeyOfficial). Genuinely one of the best Unity channels out there.

[![Watch the tutorial](https://img.youtube.com/vi/AmGSEH7QcDg/maxresdefault.jpg)](https://www.youtube.com/watch?v=AmGSEH7QcDg)
