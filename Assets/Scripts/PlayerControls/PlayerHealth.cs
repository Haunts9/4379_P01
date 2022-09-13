using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealthSystem
{
    [SerializeField] public GameObject[] art;
    private Color[] savedArt;
    bool Immunity = false;
    private void Awake()
    {
        savedArt = new Color[art.Length];
        int count = 0;
        foreach (GameObject f in art)
        {
            savedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
    }
    public override void DecreaseHealth(float amount)
    {
        if (IframeUp != true)
        {
            IframeUp = true;
            StartCoroutine(IFrame());
            _currentHealth -= amount;
            Debug.Log(gameObject.name + " Health: " + _currentHealth + " / " + _maxHealth);
            UpdateHealthBar();
            if (art != null)
            {

                Immunity = true;
                StartCoroutine(StartBlink());
            }
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    IEnumerator StartBlink()
    {
        StartCoroutine(Blink());
        yield return new WaitForSeconds(IFrameTime);
        Immunity = false;
    }
    IEnumerator Blink()
    {
        if (Immunity == true)
        {

            int count = 0;
            foreach (GameObject f in art)
            {
                f.GetComponent<Renderer>().material.color = Color.red;
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
