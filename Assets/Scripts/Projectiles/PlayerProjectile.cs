using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : ProjectileBase
{
    protected override void Connect(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null)
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
