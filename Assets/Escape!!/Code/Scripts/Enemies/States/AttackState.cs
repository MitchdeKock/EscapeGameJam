using UnityEngine;

public class AttackState : IState
{
    private EnemyBehaviour enemyBehaviour;
    private CoreHealthHandler target;
    private BaseEnemyAttack attack;

    public AttackState(EnemyBehaviour enemyBehaviour, CoreHealthHandler target, BaseEnemyAttack attack)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.target = target;
        this.attack = attack;
    }

    public void OnEnter()
    {
        Debug.Log($"{enemyBehaviour.name} has entered {this.GetType().Name}");
        attack.Refresh();
    }

    public void Tick()
    {
        if (TargetInRange() && attack.canAttack)
        {
            attack.Attack(enemyBehaviour.gameObject, target);
        }
        attack.Tick();
    }

    public void OnExit()
    {
    }

    private bool TargetInRange()
    {
        return Vector2.Distance(enemyBehaviour.transform.position, target.transform.position) <= attack.Range;
    }
}
