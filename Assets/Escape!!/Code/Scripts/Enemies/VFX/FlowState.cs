using UnityEngine;

public class FlowState : IState
{
    private readonly HealthFlowBehaviour enemyBehaviour;
    private Rigidbody2D rigidbody;
    private Transform target;

    private const float moveSpeed = 10f;

    public FlowState(HealthFlowBehaviour enemyBehaviour, Transform target)
    {
        
        this.enemyBehaviour = enemyBehaviour;
        this.target = target;
    }

    public void OnEnter()
    {
        //Debug.Log($"{enemyBehaviour.name} has entered {this.GetType().Name}");
        rigidbody = enemyBehaviour.GetComponent<Rigidbody2D>();
    }

    public void Tick()
    {
        Vector2 moveDirection = target.position - enemyBehaviour.transform.position;
        moveDirection.Normalize();
        rigidbody.velocity = moveDirection * moveSpeed;
    }
    public void TickCooldown() { }
    public void OnExit()
    {
        rigidbody.velocity = Vector2.zero;
    }
}