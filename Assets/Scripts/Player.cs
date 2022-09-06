using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    int _score = 0;
    public bool invincibility = false;
    TankController _tankController;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] public GameObject[] art;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;
    private Color[] savedArt;
    private void Awake()
    {
        savedArt = new Color[art.Length];
        int count = 0;
        _tankController = GetComponent<TankController>();
        foreach (GameObject f in art)
        {
            savedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _scoreText.text = ("Score: " + _score);
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player Health: " + _currentHealth + " / " + _maxHealth);
    }
    public void DecreaseHealth(int amount)
    {
        if (invincibility != true)
        {
            _currentHealth -= amount;
            Debug.Log("Player Health: " + _currentHealth + " / " + _maxHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill()
    {
        if (invincibility != true)
        {
           gameObject.SetActive(false);
        }

        Feedback();
    }
    private void Feedback()
    {
        //particles
        if (_deathParticles != null)
        {
            _deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
        }
        //audio
        if (_deathSound != null)
        {
            AudioHelper.PlayClip2D(_deathSound, 1f);
        }
    }
    public void ScoreUp(int value)
    {
        _score+= value;
        if (_scoreText != null)
        {
            _scoreText.text = ("Score: " + _score);
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
