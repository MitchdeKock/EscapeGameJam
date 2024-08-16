using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HealthBar healthBar;
    public event Action OnEnemyDied;

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
        Debug.Log($"{gameObject.name} was hit for {amount}."); // TODO remove debug
        healthBar.ChangeHealth(-amount);
    }

    private void OnDeath()
    {
        var coreHealth = GameObject.Find("Core").GetComponent<CoreHealthHandler>();
        coreHealth.Health += 1;
        OnEnemyDied.Invoke();
        Destroy(gameObject);
    }
}
