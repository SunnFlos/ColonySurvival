using System.IO;
using UnityEngine;
using TMPro; 
using ColonySurvival.Core.Logic;
using ColonySurvival.Core.Data;
using ColonySurvival.Data;

namespace ColonySurvival.Presentation
{
    public class ColonyGameManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI dayCounterText;
        [SerializeField] private TextMeshProUGUI foodStoreText;
        [SerializeField] private TextMeshProUGUI waterStoreText;
        [SerializeField] private TextMeshProUGUI foodDaysRemainingText;
        [SerializeField] private TextMeshProUGUI waterDaysRemainingText;
        [SerializeField] private GameObject starvingWarningPanel;

        private ColonySimulation _simulation;
        private float _timer;
        private const float TimePerDay = 1.0f; // 1 real second = 1 game day

        private void Start()
        {
            InitializeSimulation();
            UpdateUI();

            if (starvingWarningPanel != null)
                starvingWarningPanel.SetActive(false);
        }

        private void Update()
        {
            if (_simulation == null) return;

            // If colony is starving, stop advancing days automatically
            if (_simulation.IsStarving)
            {
                HandleStarvingState();
                return;
            }

            // Accelerated clock loop: 1 second = 1 game day
            _timer += Time.deltaTime;
            if (_timer >= TimePerDay)
            {
                _timer = 0f;
                _simulation.AdvanceDay();
                UpdateUI();
            }
        }

        private void InitializeSimulation()
        {
            // Load JSON from StreamingAssets folder securely
            string popPath = Path.Combine(Application.streamingAssetsPath, "population.json");
            string conPath = Path.Combine(Application.streamingAssetsPath, "consumption.json");

            PopulationData popData = JsonConfigLoader.LoadPopulationConfig(popPath);
            ConsumptionData conData = JsonConfigLoader.LoadConsumptionConfig(conPath);

            // Initialize pure core simulation instance
            _simulation = new ColonySimulation(popData, conData);
        }

        private void UpdateUI()
        {
            if (dayCounterText != null)
                dayCounterText.text = $"Day: {_simulation.CurrentDay}";

            if (foodStoreText != null)
                foodStoreText.text = $"Food: {_simulation.FoodReserve:F1}";

            if (waterStoreText != null)
                waterStoreText.text = $"Water: {_simulation.WaterReserve:F1}";

            if (foodDaysRemainingText != null)
                foodDaysRemainingText.text = $"Food Left (Days): {_simulation.GetDaysRemainingForFood():F1}";

            if (waterDaysRemainingText != null)
                waterDaysRemainingText.text = $"Water Left (Days): {_simulation.GetDaysRemainingForWater():F1}";

            if (_simulation.IsStarving)
            {
                HandleStarvingState();
            }
        }

        private void HandleStarvingState()
        {
            if (starvingWarningPanel != null)
            {
                starvingWarningPanel.SetActive(true);
            }
        }
    }
}