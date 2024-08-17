using System;
using UnityEngine;
using UnityEngine.UI;

public class ToxicVineBehaviour : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool ShowDebug;

    [Header("Stats")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float ambushRange;
    [SerializeField] private float ambushCooldown;

    private StateMachine _stateMachine;
    private CoreHealthHandler target;
    [HideInInspector] public bool isBusy = false;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackState = new VineAttackState(attackCooldown, attackRange, attackDamage, this, target);
        var ambushAttackState = new VineAmbushState(ambushCooldown, attackRange, attackDamage, this, target);
        var idleState = new VineIdleState(this);

        _stateMachine.AddAnyTransition(attackState, TargetInAttackRange());
        _stateMachine.AddAnyTransition(idleState, TargetOutOfRange());
        _stateMachine.AddAnyTransition(ambushAttackState, TargetOutOfAttackRangeInAmbushRange());

        // Start state
        _stateMachine.SetState(idleState);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetInAttackRange() => () => Vector3.Distance(transform.position, target.transform.position) <= attackRange && !isBusy;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(transform.position, target.transform.position) > ambushRange && !isBusy;
        Func<bool> TargetOutOfAttackRangeInAmbushRange() => () => Vector2.Distance(transform.position, target.transform.position) > attackRange && Vector2.Distance(transform.position, target.transform.position) <= ambushRange && !isBusy;
    }

    private void Update() => _stateMachine.Tick();

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
