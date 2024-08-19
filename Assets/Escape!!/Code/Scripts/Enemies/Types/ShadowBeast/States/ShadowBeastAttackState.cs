using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowBeastAttackState : IState
{
    private float cooldown;
    private float damage;
    private float speed;
    private float range;
    private ShadowBeastBehaviour shadowBeast;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;

    private float attackCooldownCounter;
    private float dashDurationCounter;
    private Vector2 direction;
    private Vector2 startPosition;

    public ShadowBeastAttackState(float damage, float range, float cooldown, float speed, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.cooldown = cooldown;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.shadowBeast = shadowBeast;
        this.target = target;
        this.rigidbody = rigidbody;
    }

    public void OnEnter()
    {
        shadowBeast.isBusy = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter > 0)
        {
            if (!shadowBeast.isBusy)
                attackCooldownCounter -= Time.deltaTime;
        }
        else
        {
            // ToDo attack indicator/anticipation
            SetupAttack();
            attackCooldownCounter = cooldown;
        }

        if (dashDurationCounter > 0)
        {
            rigidbody.velocity = direction * speed;
            dashDurationCounter -= Time.deltaTime;
        }
        else if(shadowBeast.isBusy)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(startPosition, shadowBeast.GetComponent<Collider2D>().bounds.max.x, direction, range);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent<CoreHealthHandler>(out CoreHealthHandler coreHealthHandler))
                {
                    coreHealthHandler.Health -= (int)damage;
                }
            }
            // ToDo exit dash animation?
            shadowBeast.isBusy = false;
        }
    }

    public void OnExit()
    {
        shadowBeast.isBusy = false;
    }

    private void SetupAttack()
    {
        // ToDo antisipation animation
        // ToDo enter dash animation
        shadowBeast.isBusy = true;
        startPosition = shadowBeast.transform.position;
        dashDurationCounter = range / speed;
        Debug.Log(dashDurationCounter);
        direction = (target.transform.position - shadowBeast.transform.position).normalized;
    }
}
