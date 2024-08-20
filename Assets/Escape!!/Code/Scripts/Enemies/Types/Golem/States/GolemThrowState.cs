using UnityEngine;

public class GolemThrowState : IState
{
    public bool canThrow => attackCooldownCounter <= 0;

    private float damage;
    private float range;
    private float cooldown;
    private RockProjectile rockProjectile;
    private GolemBehaviour golemBehaviour;
    private CoreHealthHandler target;
    private StateMachine stateMachine;
    private GolemPursuitState golemPursuitState;

    public float attackCooldownCounter;
    private float prepareAttackCooldownCounter;
    private bool isAttacking = false;
    public GolemThrowState(float damage, float range, float cooldown, RockProjectile rockProjectile, GolemBehaviour golem, CoreHealthHandler target, StateMachine stateMachine, GolemPursuitState golemPursuitState)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.rockProjectile = rockProjectile;
        this.golemBehaviour = golem;
        this.target = target;
        this.stateMachine = stateMachine;
        this.golemPursuitState = golemPursuitState;
    }

    public void OnEnter()
    {
        golemBehaviour.isBusy = isAttacking = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            attackCooldownCounter = cooldown;
            prepareAttackCooldownCounter = 1; // ToDo Replace with animation time
            golemBehaviour.isBusy = isAttacking = true;
        }

        if (isAttacking)
        {
            if (prepareAttackCooldownCounter > 0)
            {
                prepareAttackCooldownCounter -= Time.deltaTime;
            }
            else
            {
                Attack();
                golemBehaviour.isBusy = isAttacking = false;
            }
        }
    }

    public void TickCooldown()
    {
        if (attackCooldownCounter > 0 && !isAttacking)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void OnExit()
    {
        golemBehaviour.isBusy = isAttacking = false;
    }

    private void Attack()
    {
        RockProjectile projectile = GameObject.Instantiate(rockProjectile, golemBehaviour.transform.position, Quaternion.identity);
        projectile.InitializeRockProjectile(target.transform.position, damage);
        stateMachine.SetState(golemPursuitState);
    }
}
