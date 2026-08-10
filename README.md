# ColonySurvival


##  How to Run the Project
1. Clone this repository.
2. Open the project in **Unity 6000.3.7f1**
3. Open `Assets/Scenes/GameScene.unity`.
4. Press **Play**. The simulation will load config parameters automatically from `StreamingAssets`.

## 🧪 Running Unit Tests
1. Open Unity Editor menu: `Window > General > Test Runner`.
2. Select the **EditMode** tab.
3. Click **Run All** to execute simulation test suites verifying resource deduction, days remaining, and starvation triggers.

## Demo Video
[Click here to watch the 30-Second Demo Video](https://youtu.be/Ouloz5_kR4o)

## Architecture & Decisions
* **Pure Logic Separation:** All mathematical simulation models (`ColonySimulation`) live strictly in plain C# POCO classes without importing `UnityEngine` or inheriting `MonoBehaviour`.
* **Config Discipline:** Zero values are hardcoded in C# or Inspector fields. Population and consumption configurations are dynamically loaded at runtime from JSON (`population.json` & `consumption.json` in `StreamingAssets`).
* **Presentation Layer:** `ColonyGameManager` acts purely as a bridge, running the 1-second game loop and refreshing UI bindings.

## AI Tools Used
Used Claude Code and ChatGPT as For Setup Test Runner Edit Mode, Write Codes Like for SimulationTestsScript 
