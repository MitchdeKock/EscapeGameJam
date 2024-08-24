using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class ShadowBeastAmbushState : IState
{
    public bool canAmbush => attackCooldownCounter <= 0;

    private float damage;
    private float range;
    private float cooldown;

    private ShadowBeastBehaviour shadowBeastBehaviour;
    private CoreHealthHandler target;
    private HealthBar healthBar;
    private Collider2D collider;
    private Animator animator;

    private float attackCooldownCounter;
    private float despawnCounter;
    private float hideTimeCounter;
    private float warningCounter;
    private bool isAttacking;
    private Phase phase;

    public ShadowBeastAmbushState(float damage, float range, float cooldown, ShadowBeastBehaviour shadowBeastBehaviour, CoreHealthHandler target, Animator animator)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.shadowBeastBehaviour = shadowBeastBehaviour;
        this.target = target;
        healthBar = shadowBeastBehaviour.GetComponent<HealthBar>();
        collider = shadowBeastBehaviour.GetComponent<Collider2D>();
        attackCooldownCounter = cooldown;
        this.animator = animator;
    }

    public void OnEnter()
    {
        if (shadowBeastBehaviour.ShowDebug)
            Debug.Log($"{shadowBeastBehaviour.name} has entered {this.GetType().Name}");

        shadowBeastBehaviour.isBusy = isAttacking = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            animator.SetTrigger("vanish");
            despawnCounter = animator.GetCurrentAnimatorStateInfo(0).length;
            attackCooldownCounter = cooldown;
            hideTimeCounter = 1;
            phase = Phase.Invisible;
            shadowBeastBehaviour.isBusy = isAttacking = true;
        }

        switch (phase)
        {
            case Phase.Invisible:
                if (despawnCounter > 0)
                {
                    despawnCounter -= Time.deltaTime;
                }
                else
                {
                    HideShadowBeast();
                    if (hideTimeCounter > 0)
                    {
                        hideTimeCounter -= Time.deltaTime;
                    }
                    else
                    {
                        Vector3 targetPosition = target.transform.position;
                        targetPosition.z = shadowBeastBehaviour.transform.position.z;
                        shadowBeastBehaviour.transform.position = targetPosition;
                        ShowWarning();
                        phase = Phase.Attacking;
                        warningCounter = 1;
                    }
                }
                break;
            case Phase.Attacking:
                if (warningCounter > 0)
                {
                    warningCounter -= Time.deltaTime;
                }
                else
                {
                    ShowShadowBeast();
                    animator.SetTrigger("ambush");
                    Attack();
                    shadowBeastBehaviour.isBusy = isAttacking = false;
                    phase = Phase.Default;
                    HideWarning();
                }
                break;
            default:
                break;
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
        shadowBeastBehaviour.isBusy = isAttacking = false;
    }

    private void Attack()
    {
        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)damage;
        }
    }

    private void ShowWarning()
    {

    }

    private void HideWarning()
    {

    }

    private void HideShadowBeast()
    {
        collider.enabled = false;
        shadowBeastBehaviour.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        healthBar.HideHealthBar();
    }

    private void ShowShadowBeast()
    {
        collider.enabled = true;
        shadowBeastBehaviour.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        healthBar.UnHideHealthBar();
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInRange = new List<CoreHealthHandler>();

        Vector2 centerPosition = shadowBeastBehaviour.transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<CoreHealthHandler>(out CoreHealthHandler playerHealth))
            {
                objectsInRange.Add(playerHealth);
            }
        }

        return objectsInRange;
    }

    private enum Phase
    {
        Default,
        Invisible,
        Attacking
    }
}
