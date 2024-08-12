using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IState
{
    private EnemyBehaviour enemyBehaviour;
    private CoreHealthHandler target;
    private IAttack attack;
    private float attackCooldown;

    public AttackState(EnemyBehaviour enemyBehaviour, CoreHealthHandler target, IAttack attack)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.target = target;
        this.attack = attack;
    }

    public void OnEnter()
    {
        attackCooldown = 0;
    }

    public void Tick()
    {
        if (attackCooldown != 0)
        {
            attackCooldown -= Time.deltaTime;
            attackCooldown = Mathf.Clamp(attackCooldown, 0, Mathf.Infinity);
        }
        else if (TargetInRange())
        {
            target.RemoveHealth((int)attack.Damage);
            attackCooldown = attack.Cooldown;
        }
    }

    public void OnExit()
    {
    }

    private bool TargetInRange()
    {
        return Vector2.Distance(enemyBehaviour.transform.position, target.transform.position) <= attack.Range;
    }
}
