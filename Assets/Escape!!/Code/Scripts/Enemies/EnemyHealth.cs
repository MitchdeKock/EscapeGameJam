using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HealthBar healthBar;
    public event Action OnEnemyDied;
    public event Action EnemyDespawned;
    [SerializeField] private GameObject enemyFlowPrefab; // Reference to the EnemyFlow prefab

    private SpriteRenderer sprite;
    public float multiplier;

    public IEnumerator Flash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    private void Start()
    {
        Transform spriteTransform = transform.Find("Sprite");

        sprite = spriteTransform.GetComponent<SpriteRenderer>();
        healthBar = GetComponent<HealthBar>();
        healthBar.maxHealth.Value *= multiplier;
        healthBar.InitializeHealth();
        healthBar.OnDeath += OnDeath;
        healthBar.timedOut += timedOut;
    }

    public void AddHealth(float amount)
    {
        healthBar.ChangeHealth(amount);
    }

    public void RemoveHealth(float amount)
    {
        StartCoroutine(Flash());
        healthBar.ChangeHealth(-amount);
    }

    private void OnDeath()
    {
        var pos = transform.position;
        pos.z = 0;
        Instantiate(enemyFlowPrefab, pos, transform.rotation);
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }

    private void timedOut()
    {
        EnemyDespawned?.Invoke();
        Destroy(gameObject);
    }
}
