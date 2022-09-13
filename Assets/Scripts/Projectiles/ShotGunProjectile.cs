using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunProjectile : ProjectileBase
{
    [SerializeField] protected GameObject splashPellet;
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
            gameObject.transform.Rotate(new Vector3(0, 120, 0));
            Instantiate(splashPellet, (gameObject.transform.position), (gameObject.transform.rotation));
            gameObject.transform.Rotate(new Vector3(0, 30, 0));
            Instantiate(splashPellet, (gameObject.transform.position), (gameObject.transform.rotation));
            gameObject.transform.Rotate(new Vector3(0, 30, 0));
            Instantiate(splashPellet, (gameObject.transform.position), (gameObject.transform.rotation));
            gameObject.transform.Rotate(new Vector3(0, 30, 0));
            Instantiate(splashPellet, (gameObject.transform.position), (gameObject.transform.rotation));
            gameObject.transform.Rotate(new Vector3(0, 30, 0));
            Instantiate(splashPellet, (gameObject.transform.position), (gameObject.transform.rotation));

            Destroy(gameObject);
            Feedback();
        }
    }
    protected override void Feedback()
    {
        base.Feedback();
    }
}
