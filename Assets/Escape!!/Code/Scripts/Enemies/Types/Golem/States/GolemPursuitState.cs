using UnityEngine;

public class GolemPursuitState : IState
{
    private float moveSpeed;
    private GolemBehaviour golem;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;

    public GolemPursuitState(float moveSpeed, GolemBehaviour vine, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.golem = vine;
        this.target = target;
        this.rigidbody = rigidbody;
    }

    public void OnEnter()
    {
        golem.isBusy = false;
    }

    public void Tick()
    {
        Vector2 moveDirection = target.transform.position - golem.transform.position;
        rigidbody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void TickCooldown()
    {

    }

    public void OnExit()
    {
        golem.isBusy = false;
        rigidbody.velocity = Vector2.zero;
    }
}
