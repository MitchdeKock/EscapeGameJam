using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeathBarHandler : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI currHealthtext;
    public TextMeshProUGUI maxHealthtext;
    public CoreHealthHandler _coreHealth;
    public PowerUp _powerUp;
   // public Gradient _gradient;
    public Image fill;

    private int _health;
    private int _healthMax;
    private bool _isPowerUpActive;
    void Start()
    {
        _health = _coreHealth.Health;
        _healthMax = _coreHealth.MaxHealth;
        UpdateHealthUi();
        _coreHealth.OnHealthValueChanged += UpdateHealth;
        _coreHealth.OnMaxHealthValueChanged += UpdateMaxHealth;
        _powerUp.OnToggle += TogglePowerUp;
    }
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void UpdateHealth(int health)
    {
        _health = health;
        UpdateHealthUi();
    }

    void UpdateMaxHealth(int healthMax)
    {
        _healthMax = healthMax;
        UpdateHealthUi();
    }

    private void UpdateHealthUi()
    {
        SetMaxHealth(_healthMax);
        SetHealth(_health);

        if (_isPowerUpActive)
        {
            currHealthtext.color = Color.red;
            fill.color = Color.red;
        }
        else
        { 
            fill.color = Color.yellow; 
            currHealthtext.color = Color.black;
        }

        currHealthtext.text = $"{_health}";
        maxHealthtext.text = $"{_healthMax}";
    }
    void TogglePowerUp(bool toggle)
    {
        _isPowerUpActive = toggle;
        UpdateHealthUi();
    }
}
