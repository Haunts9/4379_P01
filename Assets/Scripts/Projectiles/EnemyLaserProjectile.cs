using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserProjectile : ProjectileBase
{
    protected override void Connect(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.DecreaseHealth(ProjectileDamage);
            Destroy(gameObject);
            Feedback();
        }
        else if (other.tag == "Ground")
        {
            Destroy(gameObject);
            Feedback();
        }
    }
    protected override void Feedback()
    {
        base.Feedback();
    }
}
