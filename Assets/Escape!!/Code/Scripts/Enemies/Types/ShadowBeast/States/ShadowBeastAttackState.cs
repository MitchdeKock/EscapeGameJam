using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowBeastAttackState : IState
{
    public bool canAttack => attackCooldownCounter <= 0;

    private float cooldown;
    private float damage;
    private float speed;
    private float range;
    private float damageRange;
    private ShadowBeastBehaviour shadowBeastBehaviour;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;

    private float attackCooldownCounter;
    private float attackAnimationCounter;
    private float dashDurationCounter;
    private Vector2 direction;
    private Vector2 startPosition;
    private bool isAttacking;

    private Phase phase;
    public ShadowBeastAttackState(float damage, float range, float damageRange, float cooldown, float speed, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.cooldown = cooldown;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.damageRange = damageRange;
        this.shadowBeastBehaviour = shadowBeast;
        this.target = target;
        this.rigidbody = rigidbody;
        attackCooldownCounter = cooldown;
    }

    public void OnEnter()
    {
        if (shadowBeastBehaviour.ShowDebug)
            Debug.Log($"{shadowBeastBehaviour.name} has entered {this.GetType().Name}");

        shadowBeastBehaviour.isBusy = isAttacking = false;
        phase = Phase.Default;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            attackCooldownCounter = cooldown;
            attackAnimationCounter = 1; // ToDo Replace with animation time
            shadowBeastBehaviour.isBusy = isAttacking = true;
            phase = Phase.Predash;
        }

        if (isAttacking)
        {
            switch (phase)
            {
                case Phase.Predash:
                    if (attackAnimationCounter > 0)
                    {
                        attackAnimationCounter -= Time.deltaTime;
                    }
                    else
                    {
                        startPosition = shadowBeastBehaviour.transform.position;
                        dashDurationCounter = range / speed;
                        direction = (target.transform.position - shadowBeastBehaviour.transform.position).normalized;
                        hitTargets = new List<CoreHealthHandler>();
                        phase = Phase.Dashing;
                    }
                    break;
                case Phase.Dashing:
                    if (dashDurationCounter > 0)
                    {
                        rigidbody.velocity = direction * speed; // ToDo add a curve to the speed
                        Attack();
                        dashDurationCounter -= Time.deltaTime;
                    }
                    else
                    {
                        shadowBeastBehaviour.isBusy = isAttacking = false;
                        phase = Phase.Default;
                    }
                    break;
                case Phase.Default:
                    break;
            }
        }
    }

    private List<CoreHealthHandler> hitTargets;

    public void TickCooldown()
    {
        if (attackCooldownCounter > 0 && !isAttacking)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void OnExit()
    {
        shadowBeastBehaviour.isBusy = isAttacking = false;
    }

    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(shadowBeastBehaviour.transform.position, damageRange);
        foreach(Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out CoreHealthHandler coreHealthHandler) && !hitTargets.Contains(coreHealthHandler))
            {
                coreHealthHandler.Health -= (int)damage;
                hitTargets.Add(coreHealthHandler);
            }
        }
    }

    private enum Phase
    {
        Predash,
        Dashing,
        Default
    }
}
