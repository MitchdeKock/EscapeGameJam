using UnityEngine;

[CreateAssetMenu(fileName = "New BasicEnemyMelee", menuName = "Weapons/Enemies/MeleeStaff")]
public class EnemyBasicMelee : BaseEnemyAttack
{
    [SerializeField] private float attackArcAngle;
    [SerializeField] private float damage;

    public override void Attack(GameObject attacker, CoreHealthHandler target)
    {
        target.Health -= (int)damage;
        base.Attack(attacker, target);
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Refresh()
    {
        base.Refresh();
    }
}
