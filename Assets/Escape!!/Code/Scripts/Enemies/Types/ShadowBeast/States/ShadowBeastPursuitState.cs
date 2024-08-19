using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBeastPursuitState : IState
{
    private float moveSpeed;
    private ShadowBeastBehaviour shadowBeast;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;

    public ShadowBeastPursuitState(float moveSpeed, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.shadowBeast = shadowBeast;
        this.target = target;
        this.rigidbody = rigidbody;
    }

    public void OnEnter()
    {
        shadowBeast.isBusy = false;
    }

    public void Tick()
    {
        Vector2 moveDirection = target.transform.position - shadowBeast.transform.position;
        rigidbody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void OnExit()
    {
        shadowBeast.isBusy = false;
        rigidbody.velocity = Vector2.zero;
    }
}
