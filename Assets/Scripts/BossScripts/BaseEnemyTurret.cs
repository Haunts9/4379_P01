using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyTurret : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] AudioClip fireSound;
    [SerializeField] protected float cooldownTimer = 1f;
    [SerializeField] protected GameObject barrel;
    [SerializeField] protected GameObject bullet;
    protected bool cooldown = false;

    public virtual void Shoot()
    {
        while (cooldown == false && bullet != null && barrel != null)
        {
            //Debug.Log("Pew");
            Instantiate(bullet, (barrel.transform.position), barrel.transform.rotation);
            cooldown = true;
            StartCoroutine(CooldownRotation());
            Feedback();
        }
    }

    protected void Feedback()
    {
        //particles
        if (fireParticles != null)
        {
            fireParticles = Instantiate(fireParticles, transform.position, Quaternion.identity);
        }
        //audio
        if (fireSound != null)
        {
            AudioHelper.PlayClip2D(fireSound, .1f);
        }
    }
    protected IEnumerator CooldownRotation()
    {
        yield return new WaitForSeconds(cooldownTimer);
        cooldown = false;
    }
}
