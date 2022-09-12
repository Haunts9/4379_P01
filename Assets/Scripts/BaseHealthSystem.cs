using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthSystem : MonoBehaviour
{
    [SerializeField] float _maxHealth = 3;
    [SerializeField] float IFrameTime = .5f;
    bool IframeUp = false;
    float _currentHealth;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] GameObject HealthBar;
    float healthScale;
    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealthBar();
        Debug.Log(gameObject.name + " Health: " + _currentHealth + " / " + _maxHealth);
    }
    public void DecreaseHealth(int amount)
    {
        if (IframeUp != true)
        {
            IframeUp = true;
            StartCoroutine(IFrame());
            _currentHealth -= amount;
            Debug.Log( gameObject.name + " Health: " + _currentHealth + " / " + _maxHealth);
            UpdateHealthBar();
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill()
    {

            gameObject.SetActive(false);
            DeathFeedback();
    }
    private void DeathFeedback()
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

    public void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            healthScale = _currentHealth / _maxHealth;
            //Debug.Log("Health Scale: " + healthScale);
            HealthBar.transform.localScale = new Vector3(healthScale, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
        }
    }

    IEnumerator IFrame()
    {
        yield return new WaitForSeconds(IFrameTime);
        IframeUp = false;
    }
}
