using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private BaseEnemyAttack attack;

    private StateMachine _stateMachine;
    private CoreHealthHandler target;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Core").GetComponent<CoreHealthHandler>();

        // Setup states
        var attackS = new AttackState(this, target, attack);
        var chaseS = new ChaseState(this, target.transform);

        At(chaseS, attackS, TargetReached());
        At(attackS, chaseS, TargetOutOfRange());

        _stateMachine.SetState(chaseS);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetReached() => () => Vector3.Distance(transform.position, target.transform.position) <= attack.Range;
        Func<bool> TargetOutOfRange() => () => Vector3.Distance(transform.position, target.transform.position) > attack.Range;
    }

    private void Update() => _stateMachine.Tick(); 
}