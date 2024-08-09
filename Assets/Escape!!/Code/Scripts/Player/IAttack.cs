using UnityEngine;

public interface IAttack
{
    float Cooldown { get; }
    void Attack(GameObject attacker);
}
