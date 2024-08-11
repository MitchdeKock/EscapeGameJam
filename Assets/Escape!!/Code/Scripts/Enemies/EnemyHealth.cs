using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    private event
    private void Start()
    {
        // Sub to event
        event = GameObject.Find("").GetComponent<efcore>().event;
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

        if (health <= 0)
        {
            OnKill();
        }
    }

    private void OnKill()
    {
        event.Inovke();
        Destroy(gameObject);
    }
}
