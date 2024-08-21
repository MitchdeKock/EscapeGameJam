using UnityEngine;
using UnityEngine.UI;

public class SlimePursuitState : IState
{
    private readonly SlimeBehaviour slimeBehaviour;
    private Rigidbody2D rigidbody;
    private Transform target;

    private float moveSpeed;
    private float stoppingDistance;

    public SlimePursuitState(SlimeBehaviour slimeBehaviour, Transform target, float moveSpeed, float stoppingDistance)
    {
        this.slimeBehaviour = slimeBehaviour;
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.stoppingDistance = stoppingDistance;   
    }

    public void OnEnter()
    {
        if (slimeBehaviour.ShowDebug)
            Debug.Log($"{slimeBehaviour.name} has entered {this.GetType().Name}");
        rigidbody = slimeBehaviour.GetComponent<Rigidbody2D>();
        slimeBehaviour.isBusy = false;
    }

    public void Tick()
    {
        float distance = Vector2.Distance(slimeBehaviour.transform.position, target.transform.position);
        float currentSpeed = moveSpeed;
        if (distance < stoppingDistance + 1)
        {
            currentSpeed = Mathf.Lerp(0, moveSpeed, (distance - stoppingDistance) / (1));
        }

        Vector2 moveDirection = target.transform.position - slimeBehaviour.transform.position;
        rigidbody.velocity = moveDirection.normalized * currentSpeed;
    }

    public void TickCooldown()
    {

    }

    public void OnExit()
    {
        slimeBehaviour.isBusy = false;
    }
}