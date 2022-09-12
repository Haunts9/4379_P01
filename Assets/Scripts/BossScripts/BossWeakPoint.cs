using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    [SerializeField] GameObject MyDaddy;
    BossHP DaddyHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile" && MyDaddy != null)
        {
            Debug.Log("I'm Hit!");
            DaddyHealth = MyDaddy.GetComponent<BossHP>();
            DaddyHealth.DecreaseHealth(other.GetComponent<PlayerProjectile>().ProjectileDamage);

        }
    }
}
