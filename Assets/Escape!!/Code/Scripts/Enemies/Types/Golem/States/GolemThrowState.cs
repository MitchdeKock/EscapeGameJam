using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GolemThrowState : IState
{
    private float damage;
    private float range;
    private float cooldown;
    private RockProjectile rockProjectile;
    private GolemBehaviour golem;
    private CoreHealthHandler target;
    private StateMachine stateMachine;
    private GolemPursuitState golemPursuitState;

    public float attackCooldownCounter;
    private float prepareAttackCooldownCounter;
    public bool canThrow => attackCooldownCounter <= 0;

    public GolemThrowState(float damage, float range, float cooldown, RockProjectile rockProjectile, GolemBehaviour golem, CoreHealthHandler target, StateMachine stateMachine, GolemPursuitState golemPursuitState)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.rockProjectile = rockProjectile;
        this.golem = golem;
        this.target = target;
        this.stateMachine = stateMachine;
        this.golemPursuitState = golemPursuitState;
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
            if (prepareAttackCooldownCounter > 0)
            {
                prepareAttackCooldownCounter -= Time.deltaTime;
            }
            else
            {
                Attack();
                attackCooldownCounter = cooldown;
                prepareAttackCooldownCounter = 2;
            }
        }
    }

    public void OnExit()
    {
        golem.isBusy = false;
    }

    private void Attack()
    {
        golem.isBusy = true;

        RockProjectile projectile = GameObject.Instantiate(rockProjectile, golem.transform.position, Quaternion.identity); // ToDo face throw direction
        projectile.InitializeRockProjectile(target.transform.position, damage);
        stateMachine.SetState(golemPursuitState);

        golem.isBusy = false;
    }
}
