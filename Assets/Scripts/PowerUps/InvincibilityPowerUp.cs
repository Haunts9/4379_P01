using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUpBase
{
    GameObject[] SavedColors;
    protected override void PowerUp(Player player)
    {
        player.invincibility = true;
        foreach (GameObject f in player.art)
        {
            f.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
    protected override void PowerDown(Player player)
    {
        player.invincibility = false;
        player.RevertChangesToColor();
    }
}
