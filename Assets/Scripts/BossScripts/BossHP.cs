using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : BaseHealthSystem
{
    public override void Kill()
    {

        StartCoroutine(LongDeath());
    }
    private IEnumerator LongDeath()
    {
        DeathFeedback();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
