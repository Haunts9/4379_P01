using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHP : BaseHealthSystem
{
    [SerializeField] GameObject healthDrop;
    int rando;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile" )
        {
            Debug.Log("I'm Hit!");
            DecreaseHealth(1);
        }
    }
    public override void Kill()
    {
        rando = Random.Range(1, 4);
        if (rando ==3)
        {
            Instantiate(healthDrop, (gameObject.transform.position), (gameObject.transform.rotation));
        }
        gameObject.SetActive(false);
        DeathFeedback();
    }
}
