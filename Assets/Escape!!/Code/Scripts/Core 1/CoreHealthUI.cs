using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealthUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CoreHealthHandler _coreHealth;

    private int _health;
    private int _healthMax;
    void Start()
    {
        _health = _coreHealth.Health;
        _healthMax = _coreHealth.MaxHealth;
        updateHealthUi();
        _coreHealth.OnHealthValueChanged += UpdateHealth;
        _coreHealth.OnMaxHealthValueChanged += UpdateMaxHealth;
    }

    void UpdateHealth(int health)
    {
        _health = health;
        updateHealthUi();
    }

    void UpdateMaxHealth(int healthMax)
    {
        _healthMax = healthMax;
        updateHealthUi();
    }

    private void updateHealthUi()
    {
        text.text = $"Health: {_health}/{_healthMax}";
    }
}
