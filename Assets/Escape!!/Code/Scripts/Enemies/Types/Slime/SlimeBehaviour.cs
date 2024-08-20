using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float moveSpeed;

    private StateMachine _stateMachine;
    private CoreHealthHandler target;
    private List<IState> states;

    [HideInInspector] public bool isBusy = false;
    private void Awake()
    {
        _stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackState = new SlimeAttackState(this, target, attackDamage, attackRange, attackCooldown);
        var chaseState = new SlimePursuitState(this, target.transform, moveSpeed);

        At(chaseState, attackState, TargetReached());
        At(attackState, chaseState, TargetOutOfRange());

        _stateMachine.SetState(chaseState);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetReached() => () => Vector3.Distance(transform.position, target.transform.position) <= attackDamage;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(transform.position, target.transform.position) > attackDamage;

        states = new List<IState>() { attackState, chaseState};
    }

    private void Update()
    {
        _stateMachine.Tick();

        foreach (IState state in states)
        {
            state.TickCooldown();
        }
    }
}