using UnityEngine;

public class ChaseState : IState
{
    private readonly EnemyBehaviour enemyBehaviour;
    private Rigidbody2D rigidbody;
    private Transform target;

    private const float moveSpeed = 1f;

    public ChaseState(EnemyBehaviour enemyBehaviour, Transform target)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.target = target;
    }

    public void OnEnter()
    {
        rigidbody = enemyBehaviour.GetComponent<Rigidbody2D>();
    }

    public void Tick()
    {
        Vector2 moveDirection = target.position - enemyBehaviour.transform.position;
        moveDirection.Normalize();
        rigidbody.velocity = moveDirection * moveSpeed;
    }

    public void OnExit()
    {
        rigidbody.velocity = Vector2.zero;
    }
}