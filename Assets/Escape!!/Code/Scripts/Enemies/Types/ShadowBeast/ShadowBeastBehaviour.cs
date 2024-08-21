using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShadowBeastBehaviour : EnemyBehaviour
{
    [Header("Debug")]
    public bool ShowDebug;

    [Header("Stats")]
    [SerializeField] private float dashDamage;
    [SerializeField] private float dashRange;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float ambushDamage;
    [SerializeField] private float damageRange;
    [SerializeField] private float ambushRange;
    [SerializeField] private float ambushCooldown;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackState = new ShadowBeastAttackState(dashDamage, dashRange, damageRange, dashCooldown, dashSpeed, this, target, GetComponent<Rigidbody2D>());
        var ambushState = new ShadowBeastAmbushState(ambushDamage, damageRange, ambushCooldown, this, target);
        var pursuitState = new ShadowBeastPursuitState(moveSpeed, dashRange - 1, this, target, GetComponent<Rigidbody2D>());

        stateMachine.AddAnyTransition(attackState, TargetInAttackRange());
        stateMachine.AddAnyTransition(ambushState, TargetOutOfAttackRangeInAmbushRange());
        stateMachine.AddAnyTransition(pursuitState, TargetOutOfRange());

        // Start state
        stateMachine.SetState(pursuitState);

        //void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> TargetInAttackRange() => () => Vector3.Distance(transform.position, target.transform.position) <= dashRange && !isBusy && attackState.canAttack;
        Func<bool> TargetOutOfRange() => () => (Vector3.Distance(transform.position, target.transform.position) > ambushRange || (!attackState.canAttack && !ambushState.canAmbush)) && !isBusy;
        Func<bool> TargetOutOfAttackRangeInAmbushRange() => () => Vector2.Distance(transform.position, target.transform.position) > dashRange && Vector2.Distance(transform.position, target.transform.position) <= ambushRange && ambushState.canAmbush && !isBusy;

        states = new List<IState>() { attackState, ambushState, pursuitState};
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
            Gizmos.DrawWireSphere(transform.position, damageRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, dashRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ambushRange);
        }
    }
}
