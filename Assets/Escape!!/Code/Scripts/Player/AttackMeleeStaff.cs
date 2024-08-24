using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeStaff", menuName = "Weapons/MeleeStaff")]
public class AttackMeleeStaff : BaseWeapon
{
    public override bool canAttack => attackCooldownCounter == 0;
    public float Damage { get { return damage; } set { damage = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }

    [Header("Stats")]
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private float attackArcAngle;
    [SerializeField] private float damage;
    [SerializeField] private AudioClip attackSound;

    private float attackCooldownCounter = 0;

    private void OnEnable()
    {
        cooldown = 1f;
        range = 4.5f;
        damage = 2;
        attackArcAngle = 70;
        attackCooldownCounter = 0;
    }

    public override void Attack(GameObject attacker)
    {
        foreach (EnemyHealth enemy in TargetsInRange(attacker))
        {
            enemy.RemoveHealth(damage);
        }
        SFXManager.instance.PlaySoundFXClip(attackSound, attacker.transform, 1f);
        attackCooldownCounter = cooldown;
    }
    public override void Tick()
    {
        if (attackCooldownCounter != 0)
        {
            attackCooldownCounter -= Time.deltaTime;
            attackCooldownCounter = Mathf.Clamp(attackCooldownCounter, 0, cooldown);
        }
    }

    public override void Refresh()
    {
        attackCooldownCounter = 0;
    }

    private List<EnemyHealth> TargetsInRange(GameObject attacker)
    {
        List<EnemyHealth> objectsInArc = new List<EnemyHealth>();

        Vector2 centerPosition = attacker.transform.position;
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - attacker.transform.position).normalized;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth) )
            {
                Vector2 toObject = (Vector2)collider.transform.position - centerPosition;
                float angleToObject = Vector2.Angle(direction, toObject);

                if (angleToObject <= attackArcAngle / 2f)
                {
                    objectsInArc.Add(enemyHealth);
                }
            }
        }

        return objectsInArc;
    }
}
