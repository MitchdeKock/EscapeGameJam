using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isActive = false;
    private float damage;
    private float range;
    private float speed;
    private Vector2 startPosition;
    private Rigidbody2D rb;

    void Update()
    {
        if (isActive)
        {
            rb.velocity = transform.up * speed;

            if (Vector2.Distance(startPosition, transform.position) >= range)
            {
                Destroy(gameObject);
            }
        }
    }

    public void InitializeProjectile(float damage, float range, float speed)
    {
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        isActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.RemoveHealth(damage);
        }
        Destroy(gameObject);
    }
}
