using System;
using UnityEngine;

[Serializable]
public class PlayerStatsData
{
    [field: SerializeField] public float Religion { get; set; }
    [field: SerializeField] public float People { get; set; }
    [field: SerializeField] public float Army { get; set; }
    [field: SerializeField] public float Money { get; set; }
}