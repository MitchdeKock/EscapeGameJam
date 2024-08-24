using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowBeastPursuitState : IState
{
    private float moveSpeed;
    private float stoppingDistance;
    private ShadowBeastBehaviour shadowBeastBehaviour;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public ShadowBeastPursuitState(float moveSpeed, float stoppingDistance, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.stoppingDistance = stoppingDistance;
        this.shadowBeastBehaviour = shadowBeast;
        this.target = target;
        this.rigidbody = rigidbody;
        spriteRenderer = shadowBeast.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void OnEnter()
    {
        if (shadowBeastBehaviour.ShowDebug)
            Debug.Log($"{shadowBeastBehaviour.name} has entered {this.GetType().Name}");

        shadowBeastBehaviour.isBusy = false;
    }

    public void Tick()
    {
        float distance = Vector2.Distance(shadowBeastBehaviour.transform.position, target.transform.position);
        float currentSpeed = moveSpeed;
        if (distance < stoppingDistance + 1)
        {
            currentSpeed = Mathf.Lerp(0, moveSpeed, (distance - stoppingDistance) / (1));
        }

        Vector2 moveDirection = target.transform.position - shadowBeastBehaviour.transform.position;
        rigidbody.velocity = moveDirection.normalized * currentSpeed;

        if (rigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
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
