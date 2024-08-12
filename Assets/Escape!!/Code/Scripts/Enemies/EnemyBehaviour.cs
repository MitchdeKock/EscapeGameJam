using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private StateMachine _stateMachine;
    [SerializeField] private Health target;
    [SerializeField] private IAttack attack;

    private void Awake()
    {
        attack = new AttackMeleeStaff(1, 5, 3, 70); //ToDo add attack in meaningfull way
        //ToDo Find core

        _stateMachine = new StateMachine();

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