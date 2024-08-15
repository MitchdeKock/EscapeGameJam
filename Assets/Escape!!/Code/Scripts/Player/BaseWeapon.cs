using System;
using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public abstract bool canAttack { get; }
    public abstract void Attack(GameObject attacker);
    public abstract void Tick();
    public abstract void Refresh();
    public abstract float Damage { get; set; }
    public abstract float Cooldown { get; set;}
}
