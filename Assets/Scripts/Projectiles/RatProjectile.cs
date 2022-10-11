using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatProjectile : MonoBehaviour
{

    [SerializeField] float BulletSpeed = 3f;
    [SerializeField] float DecayTime = 3f;
    [SerializeField] float MoveDelay = .5f;
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] AudioClip collisionSound;
    public float ProjectileDamage = 10f;
    private bool check = false;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Duration());
        StartCoroutine(MoveForward());
    }
    private void FixedUpdate()
    {
        ProjectileFly();
    }
    protected virtual void ProjectileFly()
    {
        if (check == true)
        {
            transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
        }
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
    protected IEnumerator Duration()
    {
        yield return new WaitForSeconds(DecayTime);
        Destroy(gameObject);
    }
    protected void Connect(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.DecreaseHealth(ProjectileDamage);
            Destroy(gameObject);
            Feedback();
        }
    }
    protected IEnumerator MoveForward()
    {
        check = true;
        yield return new WaitForSeconds(MoveDelay*2);
        StartCoroutine(Stop());
    }
    protected IEnumerator Stop()
    {
        check = false;
        yield return new WaitForSeconds(MoveDelay);
        StartCoroutine(MoveForward());
    }
}
