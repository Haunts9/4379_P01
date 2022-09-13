using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ProjectileBase
{
    [SerializeField] GameObject Guidance;
    [SerializeField] float turnSpeed = .3f;
    [SerializeField] float LockOnTime = 1f;
    [SerializeField] AudioClip LockOnSound;
    bool LockedOn = false;
    private void Awake()
    {
        StartCoroutine(Duration());
        StartCoroutine(TimetoLockon());
    }

    private void FixedUpdate()
    {
        ProjectileFly();
        if (LockedOn == true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Guidance.transform.rotation, Time.deltaTime * turnSpeed);
        }

    }
    protected override void Connect(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.DecreaseHealth(ProjectileDamage);
            Destroy(gameObject);
            Feedback();
        }
        else if (other.tag == "Ground" || other.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
            Feedback();
        }
    }
    protected override void Feedback()
    {
        base.Feedback();
    }
    protected IEnumerator TimetoLockon()
    {
        yield return new WaitForSeconds(LockOnTime);
        if (LockOnSound != null)
        {
            AudioHelper.PlayClip2D(LockOnSound, .1f);
        }
        LockedOn = true;
    }
}
