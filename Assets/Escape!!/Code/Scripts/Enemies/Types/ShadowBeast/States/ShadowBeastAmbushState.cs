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

    private float attackCooldownCounter;
    private float hideTimeCounter;
    private float warningCounter;
    private bool isAttacking;
    private Phase phase;

    public ShadowBeastAmbushState(float damage, float range, float cooldown, ShadowBeastBehaviour shadowBeast, CoreHealthHandler target)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.shadowBeastBehaviour = shadowBeast;
        this.target = target;
        healthBar = shadowBeast.GetComponent<HealthBar>();
    }

    public void OnEnter()
    {
        if (shadowBeastBehaviour.ShowDebug)
            Debug.Log($"{shadowBeastBehaviour.name} has entered {this.GetType().Name}");

        shadowBeastBehaviour.isBusy = isAttacking = false;
        attackCooldownCounter = 1;
    }

    public void Tick()
    {
        if (attackCooldownCounter <= 0)
        {
            attackCooldownCounter = cooldown;
            HideShadowBeast();
            hideTimeCounter = 1;
            phase = Phase.Invisible;
            shadowBeastBehaviour.isBusy = isAttacking = true;
        }

        switch (phase)
        {
            case Phase.Invisible:
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
                break;
            case Phase.Attacking:
                if (warningCounter > 0)
                {
                    warningCounter -= Time.deltaTime;
                }
                else
                {
                    ShowShadowBeast();
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
        shadowBeastBehaviour.transform.GetChild(0).gameObject.SetActive(false);
        healthBar.HideHealthBar();
    }

    private void ShowShadowBeast()
    {
        shadowBeastBehaviour.transform.GetChild(0).gameObject.SetActive(true);
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
