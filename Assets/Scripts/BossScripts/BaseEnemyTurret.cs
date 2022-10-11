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
    [SerializeField] protected bool Multi = false;
    protected bool cooldown = false;
    [SerializeField] GameObject[] MultiGuns;

    public virtual void Shoot()
    {
        if (Multi == false)
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
        else
        {
            if (MultiGuns != null)
            {
                foreach (GameObject Gun in MultiGuns)
                {

                    BaseEnemyTurret Trigger = Gun.GetComponent<BaseEnemyTurret>();
                    if (Trigger != null)
                    {

                        Trigger.Shoot();
                    }
                }
            }

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
