using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBeastPursuitState : IState
{
    private float moveSpeed;
    private ShadowBeastBehaviour shadowBeastBehaviour;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;

    public ShadowBeastPursuitState(float moveSpeed, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.shadowBeastBehaviour = shadowBeast;
        this.target = target;
        this.rigidbody = rigidbody;
    }

    public void OnEnter()
    {
        if (shadowBeastBehaviour.ShowDebug)
            Debug.Log($"{shadowBeastBehaviour.name} has entered {this.GetType().Name}");

        shadowBeastBehaviour.isBusy = false;
    }

    public void Tick()
    {
        Vector2 moveDirection = target.transform.position - shadowBeastBehaviour.transform.position;
        rigidbody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void TickCooldown()
    {
    }

    public void OnExit()
    {
        shadowBeastBehaviour.isBusy = false;
        rigidbody.velocity = Vector2.zero;
    }
}
