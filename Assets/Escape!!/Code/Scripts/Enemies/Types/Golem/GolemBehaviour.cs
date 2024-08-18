using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemBehaviour : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool ShowDebug;

    [Header("Stats")]
    [SerializeField] private float slamDamage;
    [SerializeField] private float slamRange;
    [SerializeField] private float slamCooldown;
    [SerializeField] private float throwDamage;
    [SerializeField] private float throwRange;
    [SerializeField] private float throwCooldown;
    [SerializeField] private RockProjectile rockProjectile;
    [SerializeField] private float moveSpeed;

    private StateMachine stateMachine;
    private CoreHealthHandler target;
    [HideInInspector] public bool isBusy = false;

    GolemThrowState throwState;
    void Start()
    {
        stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var slamState = new GolemSlamState(slamDamage, slamRange, slamCooldown, this, target);
        var pursuiState = new GolemPursuitState(moveSpeed, this, target, GetComponent<Rigidbody2D>());
        throwState = new GolemThrowState(throwDamage, throwRange, throwCooldown, rockProjectile, this, target, stateMachine, pursuiState);

        stateMachine.AddAnyTransition(slamState, TargetInAttackRange());
        stateMachine.AddAnyTransition(throwState, TargetOutOfAttackRangeInAmbushRange());
        stateMachine.AddAnyTransition(pursuiState, TargetOutOfRange());
        //At(throwState, pursuiState, TargetOutOfRangeAndThrowOnCooldown());

        // Start state
        stateMachine.SetState(pursuiState);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetInAttackRange() => () => Vector3.Distance(transform.position, target.transform.position) <= slamRange && !isBusy;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(transform.position, target.transform.position) > throwRange && !isBusy;
        //Func<bool> TargetOutOfRangeAndThrowOnCooldown() => () => Vector3.Distance(transform.position, target.transform.position) > throwRange && !isBusy && !throwState.canThrow;
        Func<bool> TargetOutOfAttackRangeInAmbushRange() => () => Vector2.Distance(transform.position, target.transform.position) > slamRange && Vector2.Distance(transform.position, target.transform.position) <= throwRange && throwState.canThrow && !isBusy;
    }

    private void Update()
    {
        if (stateMachine.CurrentState != throwState)
        {
            if (throwState.attackCooldownCounter > 0)
            {
                throwState.attackCooldownCounter -= Time.deltaTime;
            }
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
