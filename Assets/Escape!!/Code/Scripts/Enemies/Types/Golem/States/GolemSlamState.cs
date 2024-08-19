using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemSlamState : IState
{
    private float damage;
    private float range;
    private float cooldown;
    private GolemBehaviour golem;
    private CoreHealthHandler target;

    private float attackCooldownCounter;

    public GolemSlamState(float damage, float range, float cooldown, GolemBehaviour golem, CoreHealthHandler target)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.golem = golem;
        this.target = target;
    }

    public void OnEnter()
    {
        golem.isBusy = false;
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
            attackCooldownCounter = cooldown;
        }
    }

    public void OnExit()
    {
        golem.isBusy = false;
    }

    private void BasicAttack()
    {
        golem.isBusy = true;

        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)damage;
        }

        golem.isBusy = false;
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = golem.transform.position;

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
