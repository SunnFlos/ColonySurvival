using NUnit.Framework;
using ColonySurvival.Core.Logic;
using ColonySurvival.Data;

namespace ColonySurvival.Tests
{
    public class SimulationTests
    {
        [Test]
        public void Simulation_DeductsCorrectResources_AfterThreeDays()
        {
            // Arrange (Brief test case: 10 villagers, 1 food/day, 100 starting food)
            var population = new PopulationData
            {
                villagerCount = 10,
                startingFood = 100f,
                startingWater = 100f
            };

            var consumption = new ConsumptionData
            {
                foodPerVillagerPerDay = 1.0f,
                waterPerVillagerPerDay = 1.0f
            };

            var sim = new ColonySimulation(population, consumption);

            // Act: Advance 3 game days
            sim.AdvanceDay(); // Day 1 -> 2 (Removes 10 food)
            sim.AdvanceDay(); // Day 2 -> 3 (Removes 10 food)
            sim.AdvanceDay(); // Day 3 -> 4 (Removes 10 food)

            // Assert: Reserve should be exactly 70 food & water
            Assert.AreEqual(70f, sim.FoodReserve, 0.001f, "Food calculation after 3 days failed.");
            Assert.AreEqual(70f, sim.WaterReserve, 0.001f, "Water calculation after 3 days failed.");
            Assert.AreEqual(7, sim.GetDaysRemainingForFood(), 0.001f, "Days remaining calculation failed.");
            Assert.IsFalse(sim.IsStarving, "Colony should not be starving yet.");
        }

        [Test]
        public void Simulation_TriggersStarvingState_WhenFoodReachesZero()
        {
            // Arrange
            var population = new PopulationData
            {
                villagerCount = 5,
                startingFood = 10f,
                startingWater = 50f
            };

            var consumption = new ConsumptionData
            {
                foodPerVillagerPerDay = 2.0f, // 10 food consumed per day
                waterPerVillagerPerDay = 1.0f
            };

            var sim = new ColonySimulation(population, consumption);

            // Act
            sim.AdvanceDay(); // 1st day consumes 10 food -> 0 food left

            // Assert
            Assert.AreEqual(0f, sim.FoodReserve, 0.001f);
            Assert.IsTrue(sim.IsStarving, "Starving state should be true when food reaches zero.");
        }
    }
}