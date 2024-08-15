using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyAttack : BaseWeapon
{
    public override bool canAttack => attackCooldownCounter == 0;
    public override float Damage { get { return damage; } set { damage = value; } }
    public override float Cooldown { get { return cooldown; } set { cooldown = value; } }
    public float Range => range;

    [Header("Stats")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float range;
    [SerializeField] private float damage; //TODO This should not be here
    private float attackCooldownCounter = 0;

    public virtual void Attack(GameObject attacker, CoreHealthHandler target) // ToDo THIS IS UGLY FIX
    {
        Attack(attacker);
    }

    public override void Attack(GameObject attacker)
    {
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
}
