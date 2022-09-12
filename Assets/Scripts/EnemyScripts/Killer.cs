using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    protected override void PlayerImpact(PlayerHealth player)
    {
        //base.PlayerImpact(player);
        player.Kill();
    }
}
