using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected CoreHealthHandler target;
    protected List<IState> states;
    [HideInInspector] public bool isBusy;
    public float multiplier;
}
