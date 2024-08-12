using UnityEngine;

public interface IAttack
{
    float Cooldown { get; }
    float Range { get; }
    float Damage { get; }
    void Attack(GameObject attacker);
}
