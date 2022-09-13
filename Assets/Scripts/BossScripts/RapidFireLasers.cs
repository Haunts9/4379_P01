using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireLasers : BaseEnemyTurret
{
    [SerializeField] int FireCount = 5;
    [SerializeField] float FireSpeed = .2f;
    int count = 0;
    public override void Shoot()
    {
        count = 0;
        while (cooldown == false && bullet != null && barrel != null)
        {
            //Debug.Log("Pew");
            StartCoroutine(RapidFire());
            cooldown = true;
            StartCoroutine(CooldownRotation());
        }
    }
    protected IEnumerator RapidFire()
    {
        Instantiate(bullet, (barrel.transform.position), barrel.transform.rotation);
        Feedback();
        yield return new WaitForSeconds(FireSpeed);
        if (count < FireCount-1)
        {
            count++;
            StartCoroutine(RapidFire());
        }

    }
}
