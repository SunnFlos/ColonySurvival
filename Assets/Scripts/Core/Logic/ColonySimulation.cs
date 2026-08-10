using ColonySurvival.Data;

namespace ColonySurvival.Core.Logic
{
    public class ColonySimulation
    {
        public int CurrentDay { get; private set; }
        public float FoodReserve { get; private set; }
        public float WaterReserve { get; private set; }
        public int VillagerCount { get; private set; }

        private readonly float _foodConsumptionRate;
        private readonly float _waterConsumptionRate;

        public bool IsStarving => FoodReserve <= 0f || WaterReserve <= 0f;

        public ColonySimulation(PopulationData population, ConsumptionData consumption)
        {
            VillagerCount = population.villagerCount;
            FoodReserve = population.startingFood;
            WaterReserve = population.startingWater;

            _foodConsumptionRate = consumption.foodPerVillagerPerDay;
            _waterConsumptionRate = consumption.waterPerVillagerPerDay;

            CurrentDay = 1;
        }

        public void AdvanceDay()
        {
            if (IsStarving) return;

            float dailyFoodDeduction = VillagerCount * _foodConsumptionRate;
            float dailyWaterDeduction = VillagerCount * _waterConsumptionRate;

            FoodReserve = System.MathF.Max(0f, FoodReserve - dailyFoodDeduction);
            WaterReserve = System.MathF.Max(0f, WaterReserve - dailyWaterDeduction);

            CurrentDay++;
        }

        public float GetDaysRemainingForFood()
        {
            float dailyFoodDeduction = VillagerCount * _foodConsumptionRate;
            if (dailyFoodDeduction <= 0f) return float.PositiveInfinity;
            return FoodReserve / dailyFoodDeduction;
        }

        public float GetDaysRemainingForWater()
        {
            float dailyWaterDeduction = VillagerCount * _waterConsumptionRate;
            if (dailyWaterDeduction <= 0f) return float.PositiveInfinity;
            return WaterReserve / dailyWaterDeduction;
        }
    }
}