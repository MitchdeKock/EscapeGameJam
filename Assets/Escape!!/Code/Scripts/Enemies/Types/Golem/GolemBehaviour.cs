using System;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : EnemyBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool ShowDebug;
    [Space]
    [SerializeField] private Animator animator;

    [Header("Stats")]
    [SerializeField] private float slamDamage;
    [SerializeField] private float slamRange;
    [SerializeField] private float slamCooldown;
    [Space]
    [SerializeField] private float throwDamage;
    [SerializeField] private float throwRange;
    [SerializeField] private float throwCooldown;
    [SerializeField] private RockProjectile rockProjectile;
    [Space]
    [SerializeField] private float moveSpeed;

    void Start()
    {
        stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var slamState = new GolemSlamState(slamDamage, slamRange, slamCooldown, this, target, animator);
        var pursuiState = new GolemPursuitState(moveSpeed, slamRange - 1, this, target, GetComponent<Rigidbody2D>());
        var throwState = new GolemThrowState(throwDamage, throwRange, throwCooldown, rockProjectile, this, target, stateMachine, pursuiState);

        stateMachine.AddAnyTransition(slamState, TargetInAttackRange());
        stateMachine.AddAnyTransition(throwState, TargetOutOfAttackRangeInAmbushRange());
        stateMachine.AddAnyTransition(pursuiState, TargetOutOfRange());

        // Start state
        stateMachine.SetState(pursuiState);

        //void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetInAttackRange() => () => Vector3.Distance(transform.position, target.transform.position) <= slamRange && !isBusy && slamState.canAttack;
        Func<bool> TargetOutOfRange() => () => (Vector3.Distance(transform.position, target.transform.position) > throwRange || (!slamState.canAttack && !throwState.canAttack)) && !isBusy;
        //Func<bool> TargetOutOfRangeAndThrowOnCooldown() => () => Vector3.Distance(transform.position, target.transform.position) > throwRange && !isBusy && !throwState.canThrow;
        Func<bool> TargetOutOfAttackRangeInAmbushRange() => () => Vector2.Distance(transform.position, target.transform.position) > slamRange && Vector2.Distance(transform.position, target.transform.position) <= throwRange && throwState.canAttack && !isBusy;

        states = new List<IState>() { slamState, pursuiState, throwState };
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
            Gizmos.DrawWireSphere(transform.position, slamRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, throwRange);
        }
    }
}
