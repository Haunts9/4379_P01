using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    [SerializeField] GameObject MyDaddy;
    BossHP DaddyHealth;
    [SerializeField] public GameObject[] art;
    private Color[] savedArt;
    float Timer;
    bool Immunity = false;
    private void Awake()
    {
        DaddyHealth = MyDaddy.GetComponent<BossHP>();
        Timer = DaddyHealth.IFrameTime;
        savedArt = new Color[art.Length];
        int count = 0;
        foreach (GameObject f in art)
        {
            savedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile" && MyDaddy != null)
        {
            Debug.Log("I'm Hit!");

            DaddyHealth.DecreaseHealth(other.GetComponent<PlayerProjectile>().ProjectileDamage);

            if(art!=null)
            {

                Immunity = true;
                StartCoroutine(StartBlink());
            }
        }
    }
    IEnumerator StartBlink()
    {
        StartCoroutine(Blink());
        yield return new WaitForSeconds(Timer);
        Immunity = false;
    }
    IEnumerator Blink()
    {
        if (Immunity == true)
        {

            int count = 0;
            foreach (GameObject f in art)
            {
                f.GetComponent<Renderer>().material.color = Color.white;
                count++;
            }
            yield return new WaitForSeconds(.15f);
            count = 0;
            foreach (GameObject f in art)
            {
                f.GetComponent<Renderer>().material.color = savedArt[count];
                count++;
            }
            yield return new WaitForSeconds(.15f);
            StartCoroutine(Blink());
        }

    }
    public void RevertChangesToColor()
    {
        int count = 0;
        Debug.Log("Reset");
        foreach (GameObject f in art)
        {
            f.GetComponent<Renderer>().material.color = savedArt[count];
            count++;
        }
    }
}
