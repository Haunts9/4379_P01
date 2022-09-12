using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIncrease : CollectibleBase
{
    [SerializeField] int _scoreAdded = 1;
    protected override void CollectHP(PlayerHealth player)
    {

    }
    protected override void CollectScore(Player player)
    {
        player.ScoreUp(_scoreAdded);
    }
}
