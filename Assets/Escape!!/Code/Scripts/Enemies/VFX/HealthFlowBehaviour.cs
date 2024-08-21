using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HealthFlowBehaviour : MonoBehaviour
{
    private StateMachine _stateMachine;
    private CoreHealthHandler target;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        // Setup states
        var chaseS = new FlowState(this, target.transform);
        var destroyS = new DestroyState(this);

        At(chaseS, destroyS, TargetReached());

        _stateMachine.SetState(chaseS);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetReached() => () => Vector3.Distance(transform.position, target.transform.position) <= 0.1;
    }

    private void Update() => _stateMachine.Tick();
    }
