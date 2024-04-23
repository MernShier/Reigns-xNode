using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Slider religionSlider;
        [SerializeField] private Slider peopleSlider;
        [SerializeField] private Slider armySlider;
        [SerializeField] private Slider moneySlider;

        private void Start()
        {
            SetSlidersMinValue();
            SetSlidersMaxValue();
            player.OnPlayerStatsChange += UpdateSliders;
            UpdateSliders();
        }

        private void SetSlidersMinValue()
        {
            religionSlider.minValue = player.StatsMinValue;
            peopleSlider.minValue = player.StatsMinValue;
            armySlider.minValue = player.StatsMinValue;
            moneySlider.minValue = player.StatsMinValue;
        }

        private void SetSlidersMaxValue()
        {
            religionSlider.maxValue = player.StatsMaxValue;
            peopleSlider.maxValue = player.StatsMaxValue;
            armySlider.maxValue = player.StatsMaxValue;
            moneySlider.maxValue = player.StatsMaxValue;
        }

        private void UpdateSliders()
        {
            religionSlider.value = player.PlayerCurrentStats.Religion;
            peopleSlider.value = player.PlayerCurrentStats.People;
            armySlider.value = player.PlayerCurrentStats.Army;
            moneySlider.value = player.PlayerCurrentStats.Money;
        }
    }
}