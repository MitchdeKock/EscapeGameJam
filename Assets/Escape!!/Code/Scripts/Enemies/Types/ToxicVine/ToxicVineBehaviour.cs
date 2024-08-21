using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ToxicVineBehaviour : EnemyBehaviour
{
    [Header("Debug")]
    public bool ShowDebug;

    [Header("Stats")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [Space]
    [SerializeField] private float ambushDamage;
    [SerializeField] private float ambushRange;
    [SerializeField] private float ambushCooldown;

    private void Awake()
    {
        stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackState = new VineAttackState(attackCooldown, attackRange, attackDamage, this, target);
        var ambushState = new VineAmbushState(ambushCooldown, attackRange, ambushDamage, this, target);
        var idleState = new VineIdleState(this);

        stateMachine.AddAnyTransition(attackState, TargetInAttackRange());
        stateMachine.AddAnyTransition(idleState, TargetOutOfRange());
        stateMachine.AddAnyTransition(ambushState, TargetOutOfAttackRangeInAmbushRange());

        // Start state
        stateMachine.SetState(idleState);

        Func<bool> TargetInAttackRange() => () => Vector3.Distance(transform.position, target.transform.position) <= attackRange && !isBusy;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(transform.position, target.transform.position) > ambushRange && !isBusy;
        Func<bool> TargetOutOfAttackRangeInAmbushRange() => () => Vector2.Distance(transform.position, target.transform.position) > attackRange && Vector2.Distance(transform.position, target.transform.position) <= ambushRange && !isBusy;

        states = new List<IState>() { attackState, ambushState, idleState };
    }

    private void Update()
    {
        foreach (IState state in states)
        {
            state.TickCooldown();
        }

        stateMachine.Tick();
    }

    private void OnDrawGizmos()
    {
        if (ShowDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ambushRange);
        }
    }
}
