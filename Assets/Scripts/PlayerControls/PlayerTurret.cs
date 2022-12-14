using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] AudioClip fireSound;
    [SerializeField] float cooldownTimer = .2f;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject bullet;
    bool cooldown = false;

    private void FixedUpdate()
    {
        Shoot();
    }
    private void Shoot()
    {
        while (cooldown == false && Input.GetKey("space") && bullet != null && barrel != null)
        {
            //Debug.Log("Pew");
            Instantiate(bullet, (barrel.transform.position), barrel.transform.rotation);
            cooldown = true;
            StartCoroutine(CooldownRotation());
            Feedback();
        }
    }

    private void Feedback()
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
    IEnumerator CooldownRotation()
    {
        yield return new WaitForSeconds(cooldownTimer);
        cooldown = false;
    }
}
