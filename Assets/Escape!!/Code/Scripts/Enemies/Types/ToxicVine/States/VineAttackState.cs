using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class VineAttackState : IState
{
    public bool canAttack { get => attackCooldownCounter <= 0; }

    private float attackCooldown;
    private float attackRange;
    private float attackDamage;
    private ToxicVineBehaviour vine;
    private CoreHealthHandler target;

    private float attackCooldownCounter;

    public VineAttackState(float attackCooldown, float attackRange, float attackDamage, ToxicVineBehaviour vine, CoreHealthHandler target)
    {
        this.attackCooldown = attackCooldown;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.vine = vine;
        this.target = target;
    }

    public void OnEnter()
    {
        Debug.Log($"{vine.name} has entered {this.GetType().Name}");
        attackCooldownCounter = attackCooldown;
        vine.isBusy = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter > 0)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
        else
        {
            // ToDo attack indicator/anticipation
            BasicAttack();
            attackCooldownCounter = attackCooldown;
        }
    }

    public void OnExit()
    {
        vine.isBusy = false;
    }

    private void BasicAttack()
    {
        vine.isBusy = true;

        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)attackDamage;
        }

        vine.isBusy = false;
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = vine.transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, attackRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<CoreHealthHandler>(out CoreHealthHandler playerHealth))
            {
                objectsInArc.Add(playerHealth);
            }
        }

        return objectsInArc;
    }
}
