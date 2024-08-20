using UnityEngine;
using UnityEngine.UI;

public class SlimePursuitState : IState
{
    private readonly SlimeBehaviour slimeBehaviour;
    private Rigidbody2D rigidbody;
    private Transform target;

    private float moveSpeed;

    public SlimePursuitState(SlimeBehaviour slimeBehaviour, Transform target, float moveSpeed)
    {
        this.slimeBehaviour = slimeBehaviour;
        this.target = target;
        this.moveSpeed = moveSpeed;
    }

    public void OnEnter()
    {
        Debug.Log($"{slimeBehaviour.name} has entered {this.GetType().Name}");
        rigidbody = slimeBehaviour.GetComponent<Rigidbody2D>();
        slimeBehaviour.isBusy = false;
    }

    public void Tick()
    {
        Vector2 moveDirection = target.position - slimeBehaviour.transform.position;
        rigidbody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void TickCooldown()
    {

    }

    public void OnExit()
    {
        slimeBehaviour.isBusy = false;
    }
}