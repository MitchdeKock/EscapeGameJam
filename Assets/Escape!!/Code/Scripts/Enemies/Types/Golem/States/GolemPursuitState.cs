using UnityEngine;

public class GolemPursuitState : IState
{
    private float moveSpeed;
    private float stoppingDistance;
    private GolemBehaviour golemBehaviour;
    private CoreHealthHandler target;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public GolemPursuitState(float moveSpeed, float stoppingDistance, GolemBehaviour vine, CoreHealthHandler target, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.golemBehaviour = vine;
        this.target = target;
        this.rigidbody = rigidbody;
        this.stoppingDistance = stoppingDistance;
        spriteRenderer = golemBehaviour.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void OnEnter()
    {
        golemBehaviour.isBusy = false;
    }

    public void Tick()
    {
        float distance = Vector2.Distance(golemBehaviour.transform.position, target.transform.position);
        float currentSpeed = moveSpeed;
        if (distance < stoppingDistance + 1)
        {
            currentSpeed = Mathf.Lerp(0, moveSpeed, (distance - stoppingDistance) / (1));
        }

        Vector2 moveDirection = target.transform.position - golemBehaviour.transform.position;
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
        golemBehaviour.isBusy = false;
        rigidbody.velocity = Vector2.zero;
    }
}
