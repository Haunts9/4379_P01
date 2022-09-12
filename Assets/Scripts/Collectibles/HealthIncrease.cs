using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectibleBase
{
    [SerializeField] int _healthAdded = 1;

    protected override void CollectHP(PlayerHealth player)
    {
        player.IncreaseHealth(_healthAdded);
    }
    protected override void CollectScore(Player player)
    {
    }
}
