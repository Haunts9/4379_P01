using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIncrease : CollectibleBase
{
    [SerializeField] int _scoreAdded = 1;

    protected override void Collect(Player player)
    {
        player.ScoreUp(_scoreAdded);
    }
}
