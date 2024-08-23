using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : EnemyBehaviour
{
    [Header("Debug")]
    public bool ShowDebug;

    [Header("Stats")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackState = new SlimeAttackState(this, target, attackDamage, attackRange, attackCooldown);
        var chaseState = new SlimePursuitState(this, target.transform, moveSpeed, attackRange - 0.5f);

        At(chaseState, attackState, TargetReached());
        At(attackState, chaseState, TargetOutOfRange());

        stateMachine.SetState(chaseState);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetReached() => () => Vector3.Distance(transform.position, target.transform.position) <= attackRange && !isBusy && attackState.canAttack;
        Func<bool> TargetOutOfRange() => () => (Vector3.Distance(transform.position, target.transform.position) > attackRange || (!attackState.canAttack)) && !isBusy;

        states = new List<IState>() { attackState, chaseState};
    }

    private void Update()
    {
        stateMachine.Tick();

        foreach (IState state in states)
        {
            state.TickCooldown();
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}