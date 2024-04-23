using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerStatsChange;
    [field: SerializeField] public float StatsMaxValue { get; private set; }
    [field: SerializeField] public float StatsMinValue { get; private set; }
    [field: SerializeField] public PlayerStatsData PlayerCurrentStats { get; private set; }
    [field: SerializeField] public bool Dead { get; private set; }

    public void ChangePlayerStats(PlayerStatsData changeData)
    {
        PlayerCurrentStats.Religion =
            Mathf.Clamp(changeData.Religion + PlayerCurrentStats.Religion, StatsMinValue, StatsMaxValue);
        PlayerCurrentStats.People =
            Mathf.Clamp(changeData.People + PlayerCurrentStats.People, StatsMinValue, StatsMaxValue);
        PlayerCurrentStats.Army =
            Mathf.Clamp(changeData.Army + PlayerCurrentStats.Army, StatsMinValue, StatsMaxValue);
        PlayerCurrentStats.Money =
            Mathf.Clamp(changeData.Money + PlayerCurrentStats.Money, StatsMinValue, StatsMaxValue);
        CheckDeath();
        OnPlayerStatsChange?.Invoke();
    }

    public void ReversePlayerStats(PlayerStatsData changeData)
    {
        PlayerCurrentStats.Religion -= changeData.Religion;
        PlayerCurrentStats.People -= changeData.People;
        PlayerCurrentStats.Army -= changeData.Army;
        PlayerCurrentStats.Money -= changeData.Money;
        OnPlayerStatsChange?.Invoke();
    }

    public void Revive()
    {
        Dead = false;
    }

    private void CheckDeath()
    {
        if (PlayerCurrentStats.Religion <= StatsMinValue
            || PlayerCurrentStats.Religion >= StatsMaxValue
            || PlayerCurrentStats.People <= StatsMinValue
            || PlayerCurrentStats.People >= StatsMaxValue
            || PlayerCurrentStats.Army <= StatsMinValue
            || PlayerCurrentStats.Army >= StatsMaxValue
            || PlayerCurrentStats.Money <= StatsMinValue
            || PlayerCurrentStats.Money >= StatsMaxValue
           )
        {
            Death();
        }
    }

    private void Death()
    {
        Dead = true;
    }
}