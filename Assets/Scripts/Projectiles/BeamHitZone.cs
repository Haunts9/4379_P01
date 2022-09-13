using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHitZone : MonoBehaviour
{
    [SerializeField] float DamageTickRate = .1f;
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] AudioClip collisionSound;
    public float ProjectileDamage = 5f;
    bool isPlayerInBeam = false;
    bool startDamage = false;

    private void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            isPlayerInBeam = true;
            if(startDamage==false)
            {
            startDamage = true;
            StartCoroutine(BeamDamage(player, other));
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            isPlayerInBeam = false;

        }
    }
    protected void Connect(Collider other)
    {

    }
    protected virtual void Feedback(Transform player)
    {
        //particles
        if (collisionParticles != null)
        {
            collisionParticles = Instantiate(collisionParticles, player.position, Quaternion.identity);
        }
        //audio
        if (collisionSound != null)
        {
            AudioHelper.PlayClip2D(collisionSound, .1f);
        }
    }
    protected IEnumerator BeamDamage(PlayerHealth player, Collider other)
    {
        player.DecreaseHealth(ProjectileDamage);
        Feedback(other.transform);
        yield return new WaitForSeconds(DamageTickRate);
        if (isPlayerInBeam == true)
        {
            StartCoroutine(BeamDamage(player,other));
        }
        else
        {
            startDamage = false;
        }
    }
}
