using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackRangedStaff : IAttack
{
    public float Cooldown => cooldown;
    [SerializeField] private readonly float cooldown;
    [SerializeField] private readonly float damage;
    [SerializeField] private readonly float range;
    [SerializeField] private readonly Projectile projectile;

    public AttackRangedStaff(float cooldown, float damage, float range, Projectile projectile)
    {
        this.cooldown = cooldown;
        this.damage = damage;
        this.range = range;
        this.projectile = projectile;
    }

    public void Attack(GameObject attacker)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - attacker.transform.position;
        Quaternion projectileRotation = Quaternion.LookRotation(Vector3.forward, relativeMouseWorldPosition);

        Projectile projectile = GameObject.Instantiate<Projectile>(this.projectile, attacker.transform.position, projectileRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), attacker.GetComponent<Collider2D>());
        projectile.InitializeProjectile(damage, range, 10);
    }
}
