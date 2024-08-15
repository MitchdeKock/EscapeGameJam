using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponent<HealthBar>();
        healthBar.OnDeath += OnDeath;
    }

    public void AddHealth(float amount)
    {
        healthBar.ChangeHealth(amount);
    }

    public void RemoveHealth(float amount)
    {
        Debug.Log($"{gameObject.name} was hit for {amount}.");

        healthBar.ChangeHealth(-amount);
    }

    private void OnDeath()
    {
        var coreHealth = GameObject.Find("Core").GetComponent<CoreHealthHandler>();
        coreHealth.ChangeHealth();
        Destroy(gameObject);
    }
}
