# 🚗 Drive Thru

A 2D drive-thru restaurant management game prototype built with **Unity** and **C#**.

In Drive Thru, players manage a fast-paced drive-thru restaurant, handle customer orders, prepare food, coordinate chefs, assemble orders, process payments, and serve customers before they lose patience.

🎮 **[Play Drive Thru on itch.io](https://debuggeddream.itch.io/drive-thru)**

---

## 📸 About the Game

Drive Thru is a restaurant management and time-management game centered around a collection of interconnected gameplay systems.

The player manages the entire drive-thru operation, from receiving a customer's order to preparing food and delivering the completed order.

The prototype explores how different gameplay mechanics can work together to create a complete restaurant management experience.

## 🎮 Gameplay

The main gameplay loop consists of the following stages:

1. **Customer Arrival** – Cars arrive at the drive-thru and enter the ordering process.
2. **Order Management** – Customers generate orders containing different food items.
3. **POS System** – The player enters the requested items and generates the bill.
4. **Food Preparation** – Chefs receive ingredients and prepare food according to recipes.
5. **Food Management** – Finished food is stored and made available for packing.
6. **Order Packing** – The player assembles the required food items using a packing grid.
7. **Payment Processing** – Customers pay through the payment machine.
8. **Take-out & Delivery** – The completed order is checked and handed over to the customer.

## ⚙️ Core Game Systems

### 🚘 1. Customer & Car Management
- Cars follow predefined waypoints through the drive-thru.
- Customers progress through different service stages.
- Cars move through the ordering, payment and take-out areas.
- Customer names and order information are generated dynamically.
- Car states control the customer's progression through the drive-thru.

### 🧾 2. POS & Order Management
- Customers generate food orders.
- The POS interface allows the player to enter requested food items.
- Orders are converted into bills with calculated prices.
- Each customer has an associated order and payment information.

### 👨‍🍳 3. Chef & Cooking System
- Chefs receive ingredients and prepare food using predefined recipes.
- Ingredients are assigned to chefs through drag-and-drop interactions.
- Each chef tracks the ingredients it receives.
- Recipes determine which food item can be prepared.
- Cooking takes time, represented through cooking timers.
- Multiple chefs allow different food items to be prepared independently.

### 🍔 4. Ingredient & Recipe System

The prototype includes three primary food categories:

| Food Item | Ingredients |
|---|---|
| Burger | Bun, Patty, Lettuce, Cheese |
| Fries | Potato, Oil |
| Cola | Cola |

Ingredients are combined according to their respective recipes to produce finished food items.

### 📦 5. Food Storage & Packing
- Finished food items are stored in designated holders.
- The packing grid allows the player to assemble customer orders.
- Items can be selected and placed into the packing area.
- A trash bin allows unwanted items to be discarded.
- The packing process ensures the correct items are prepared for each customer.

### 💳 6. Payment System
- Customers proceed to the payment station.
- A payment machine handles card-based transactions.
- The player interacts with a credit card object and drags it to the scanning area.
- Payment is processed before the customer proceeds to take-out.

### 🪟 7. Take-out System
- The final stage of the customer journey.
- The packed order is checked before delivery.
- Once the order is completed, the customer leaves the drive-thru.

## 🛠️ Technologies Used

- **Game Engine:** Unity
- **Programming Language:** C#
- **Game Type:** 2D
- **Interaction:** Drag and Drop
- **Development Environment:** Visual Studio
- **Version Control:** Git & GitHub
- **Distribution:** itch.io (HTML5)

## 📂 Project Structure

The Unity project follows the standard Unity project structure:

```text
drive-thru-prototype/
│
├── Assets/
│   ├── Animations/
│   ├── Audio/
│   ├── Materials/
│   ├── Prefabs/
│   ├── Scenes/
│   ├── Scripts/
│   │   ├── Car/
│   │   ├── Chef/
│   │   ├── Cooking/
│   │   ├── POS/
│   │   ├── Packing/
│   │   ├── Payment/
│   │   └── UI/
│   ├── Sprites/
│   └── ...
│
├── Packages/
├── ProjectSettings/
├── UserSettings/
└── README.md
```

*The directory above is a conceptual overview of the gameplay-related organization, not an exact inventory of every folder in the repository.*

## 🚀 Running the Project

To explore the Unity project locally:

1. Clone this repository:

```bash
git clone https://github.com/malisamarth/drive-thru-prototype.git
```

2. Open Unity Hub.
3. Select **Add Project from Disk**.
4. Choose the cloned project directory.
5. Open the project using a compatible Unity version.
6. Open the main gameplay scene and run the project.

Alternatively, play the published prototype directly:

**[▶ Play Drive Thru](https://debuggeddream.itch.io/drive-thru)**

## 🔮 Future Development

This repository represents the initial playable prototype and serves as the foundation for a more complete game.

The planned full-game development includes:

- Rebuilding the game in a fresh Unity 2D project.
- Improved system architecture and data management.
- More food items, ingredients and recipes.
- Customer patience and satisfaction mechanics.
- Shift-based gameplay and performance evaluation.
- Shop progression and chef upgrades.
- Additional payment methods and order modifiers.
- Improved UI, animations, audio and feedback.
- Story-driven progression and unlockable content.

The initial target for the full game is **PC**, with a possible mobile port after release.

## 👨‍💻 Developer

**Samarth Mali**

Game Developer | Unity | C#

- GitHub: [@malisamarth](https://github.com/malisamarth)
- Game: [Drive Thru – itch.io](https://debuggeddream.itch.io/drive-thru)

---

*Built as a game development project to explore gameplay systems, interactive mechanics, and the design of a restaurant management game.*
