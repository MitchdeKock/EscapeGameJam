using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineIdleState : IState
{
    private ToxicVineBehaviour vine;

    public VineIdleState(ToxicVineBehaviour vine)
    {
        this.vine = vine;
    }

    public void OnEnter()
    {
        Debug.Log($"{vine.name} has entered {this.GetType().Name}");
        //Todo trigger animation
    }

    public void Tick()
    {
    }

    public void OnExit()
    {
    }
}
