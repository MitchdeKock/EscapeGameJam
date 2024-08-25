using System.Collections.Generic;
using UnityEngine;

public class GolemSlamState : IState
{
    public bool canAttack => attackCooldownCounter <= 0;

    private float damage;
    private float range;
    private float cooldown;
    private GolemBehaviour golemBehaviour;
    private CoreHealthHandler target;
    private Animator animator;

    private float attackCooldownCounter;
    private float attackAnimationCounter;
    private bool isAttacking = false;

    public GolemSlamState(float damage, float range, float cooldown, GolemBehaviour golem, CoreHealthHandler target, Animator animator)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.golemBehaviour = golem;
        this.target = target;
        attackCooldownCounter = cooldown;
        this.animator = animator;
    }

    public void OnEnter()
    {
        golemBehaviour.isBusy = isAttacking  = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            attackCooldownCounter = cooldown;
            attackAnimationCounter = 1; // ToDo Replace with animation time
            golemBehaviour.isBusy = isAttacking = true;
        }

        if (isAttacking)
        {
            if (attackAnimationCounter > 0)
            {
                attackAnimationCounter -= Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("attack");
                Attack();
                golemBehaviour.isBusy = isAttacking = false;
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
        golemBehaviour.isBusy = isAttacking = false;
    }

    private void Attack()
    {
        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)damage;
        }

        golemBehaviour.isBusy = isAttacking = false;
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = golemBehaviour.transform.position;

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
