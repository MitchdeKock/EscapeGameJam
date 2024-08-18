using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealthUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CoreHealthHandler _coreHealth;
    public PowerUp _powerUp;

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
        var currText = $"Health: {_health}/{_healthMax}";
        if (_isPowerUpActive) currText += "***";
        text.text = currText;
    }
    void TogglePowerUp(bool toggle)
    {
        _isPowerUpActive = toggle;
        UpdateHealthUi();
    }
}
