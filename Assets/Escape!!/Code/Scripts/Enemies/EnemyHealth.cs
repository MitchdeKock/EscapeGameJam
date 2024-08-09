using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void AddHealth(float amount)
    {
        health += amount;
    }

    public void RemoveHealth(float amount)
    {
        Debug.Log($"{gameObject.name} was hit and now has {health}/{maxHealth} hp.");
        health -= amount;
    }
}
