using UnityEngine;

public class SlimeAttackState : IState
{
    public bool canAttack => attackCooldownCounter <= 0;
    private SlimeBehaviour enemyBehaviour;
    private CoreHealthHandler target;
    private float damage;
    private float range;
    private float cooldown;
    
    private float attackCooldownCounter;

    public SlimeAttackState(SlimeBehaviour enemyBehaviour, CoreHealthHandler target, float damage, float range, float cooldown)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.target = target;
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
    }

    public void OnEnter()
    {
        Debug.Log($"{enemyBehaviour.name} has entered {this.GetType().Name}");
        enemyBehaviour.isBusy = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0 && TargetInRange())
        {
            target.Health -= (int)damage;
            attackCooldownCounter = cooldown;
        }
    }

    public void TickCooldown()
    {
        if (attackCooldownCounter > 0)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void OnExit()
    {
        enemyBehaviour.isBusy = false;
    }

    private bool TargetInRange()
    {
        return Vector2.Distance(enemyBehaviour.transform.position, target.transform.position) <= range;
    }
}
