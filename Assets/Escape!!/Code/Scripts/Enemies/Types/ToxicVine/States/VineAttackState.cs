using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttackState : IState
{
    public bool canAttack { get => attackCooldownCounter <= 0; }

    private float damage;
    private float range;
    private float cooldown;
    private ToxicVineBehaviour vineBehaviour;
    private CoreHealthHandler target;
    private Animator animator;

    private float attackCooldownCounter;
    private float attackAnimationCounter;
    private bool isAttacking = false;

    public VineAttackState(float cooldown, float range, float damage, ToxicVineBehaviour vine, CoreHealthHandler target, Animator animator)
    {
        this.cooldown = cooldown;
        this.range = range;
        this.damage = damage;
        this.vineBehaviour = vine;
        this.target = target;
        this.animator = animator;
    }

    public void OnEnter()
    {
        if (vineBehaviour.ShowDebug)
            Debug.Log($"{vineBehaviour.name} has entered {this.GetType().Name}");

        attackCooldownCounter = 1;
        vineBehaviour.isBusy = isAttacking = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            attackCooldownCounter = cooldown;
            attackAnimationCounter = 1; // ToDo Replace with animation time
            vineBehaviour.isBusy = isAttacking = true;
        }

        if (isAttacking)
        {
            if (attackAnimationCounter > 0)
            {
                attackAnimationCounter -= Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("Attack");
                Attack();
                vineBehaviour.isBusy = isAttacking = false;
            }
        }
    }

    public void TickCooldown()
    {
        if (attackCooldownCounter > 0 && !isAttacking)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void OnExit()
    {
        vineBehaviour.isBusy = isAttacking = false;
    }

    private void Attack()
    {
        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)damage;
        }

        vineBehaviour.isBusy = false;
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = vineBehaviour.transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, range);

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
