using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] float _speedAmount = .1f;
    protected override void PlayerImpact(Player player)
    {
        //base.PlayerImpact(player);
        TankController controller = player.GetComponent<TankController>();
        if (controller != null && player.invincibility != true)
        {
            controller.MaxSpeed -= _speedAmount;
        }
    }
}
