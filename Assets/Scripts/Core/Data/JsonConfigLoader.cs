using System.IO;
using ColonySurvival.Data;
using UnityEngine;
namespace ColonySurvival.Core.Data
{
    public static class JsonConfigLoader
    {
        public static PopulationData LoadPopulationConfig(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"[JsonConfigLoader] Config file not found at: {fullPath}");
            }

            string jsonContent = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<PopulationData>(jsonContent);
        }

        public static ConsumptionData LoadConsumptionConfig(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"[JsonConfigLoader] Config file not found at: {fullPath}");
            }

            string jsonContent = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<ConsumptionData>(jsonContent);
        }
    }
}