using System.Collections.Generic;
using UnityEngine;

public class AttackMeleeStaff : IAttack
{
    public float Cooldown => cooldown;
    public float Range => range;
    public float Damage => damage;

    [SerializeField] private readonly float cooldown;
    [SerializeField] private readonly float damage;
    [SerializeField] private readonly float range;
    [SerializeField] private readonly float swingSize;

    public AttackMeleeStaff(float cooldown, float damage, float range, float swingSize)
    {
        this.cooldown = cooldown;
        this.damage = damage;
        this.range = range;
        this.swingSize = swingSize;
    }

    public void Attack(GameObject attacker)
    {
        foreach (EnemyHealth enemy in TargetsInRange(attacker))
        {
            enemy.RemoveHealth(damage);
        }
    }

    private List<EnemyHealth> TargetsInRange(GameObject attacker)
    {
        List<EnemyHealth> objectsInArc = new List<EnemyHealth>();

        Vector2 centerPosition = attacker.transform.position;
        Vector2 direction = attacker.transform.up;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, range/*, detectionLayer*/); // ToDo Assign enemies to a layer

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth) )
            {
                Vector2 toObject = (Vector2)collider.transform.position - centerPosition;
                float angleToObject = Vector2.Angle(direction, toObject);

                if (angleToObject <= swingSize / 2f)
                {
                    objectsInArc.Add(enemyHealth);
                }
            }
        }

        return objectsInArc;
    }
}
