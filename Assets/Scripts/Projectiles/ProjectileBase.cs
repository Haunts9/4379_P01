using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract void Connect(Collider other);

    [SerializeField] float BulletSpeed = 3f;
    [SerializeField] float DecayTime = 3f;
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] AudioClip collisionSound;
    public float ProjectileDamage = 10f;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Duration());
    }
    private void FixedUpdate()
    {
        ProjectileFly();
    }
    private void ProjectileFly()
    {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerProjectile" && other.tag != "EnemyProjectile")
        {

           // Debug.Log("hit " + other.name);
            Connect(other);

        }
    }
    protected virtual void Feedback()
    {
        //particles
        if (collisionParticles != null)
        {
            collisionParticles = Instantiate(collisionParticles, transform.position, Quaternion.identity);
        }
        //audio
        if (collisionSound != null)
        {
            AudioHelper.PlayClip2D(collisionSound, .1f);
        }
    }
    IEnumerator Duration()
    {
        yield return new WaitForSeconds(DecayTime);
        Destroy(gameObject);
    }
}
