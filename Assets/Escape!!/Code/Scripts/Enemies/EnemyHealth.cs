using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HealthBar healthBar;
    public event Action OnEnemyDied;
    [SerializeField] private GameObject enemyFlowPrefab; // Reference to the EnemyFlow prefab

    private SpriteRenderer sprite;

    public IEnumerator Flash()
    {
        Color oldColour = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = oldColour;
    }

    private void Start()
    {
        Transform spriteTransform = transform.Find("Sprite");

        sprite = spriteTransform.GetComponent<SpriteRenderer>();
        healthBar = GetComponent<HealthBar>();
        healthBar.OnDeath += OnDeath;
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
        var coreHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        coreHealth.Health += 1;
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}
