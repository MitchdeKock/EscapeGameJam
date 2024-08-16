using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangeStaff", menuName = "Weapons/RangeStaff")]
public class AttackRangedStaff : BaseWeapon
{
    public override bool canAttack => attackCooldownCounter == 0;
    public float Damage { get { return damage; } set { damage = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }

    [Header("Stats")]
    [SerializeField] private Projectile Projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    private float attackCooldownCounter = 0;
    public override void Attack(GameObject attacker)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - attacker.transform.position;
        Quaternion projectileRotation = Quaternion.LookRotation(Vector3.forward, relativeMouseWorldPosition);

        Projectile projectile = GameObject.Instantiate<Projectile>(this.Projectile, attacker.transform.position, projectileRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), attacker.GetComponent<Collider2D>());
        projectile.InitializeProjectile(damage, range, 10); // ToDo Assign enemies to a layer

        attackCooldownCounter = cooldown;
    }
    public override void Tick()
    {
        if (attackCooldownCounter != 0)
        {
            attackCooldownCounter -= Time.deltaTime;
            attackCooldownCounter = Mathf.Clamp(attackCooldownCounter, 0, cooldown);
        }
    }
    public override void Refresh()
    {
        attackCooldownCounter = 0;
    }
}
