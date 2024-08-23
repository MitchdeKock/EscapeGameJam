using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineIdleState : IState
{
    private ToxicVineBehaviour vineBehaviour;

    public VineIdleState(ToxicVineBehaviour vine)
    {
        this.vineBehaviour = vine;
    }

    public void OnEnter()
    {
        if (vineBehaviour.ShowDebug)
            Debug.Log($"{vineBehaviour.name} has entered {this.GetType().Name}");
    }

    public void Tick()
    {
    }

    public void TickCooldown()
    {
    }

    public void OnExit()
    {
    }

}
