using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ShadowBeastAmbushState : MonoBehaviour
{
    private float damage;
    private float range;
    private float cooldown;

    private ShadowBeastBehaviour shadowBeast;
    private CoreHealthHandler target;

    private float attackCooldownCounter;
    private float hideTimeCounter;
    private float attackDelayCounter;
    public void OnEnter()
    {
        shadowBeast.isBusy = false;
    }

    public void Tick()
    {
        if (attackCooldownCounter > 0 && !shadowBeast.isBusy)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
        else
        {
            SetupAttack();
            attackCooldownCounter = cooldown;
        }

        if (hideTimeCounter > 0)
        {
            hideTimeCounter -= Time.deltaTime;
        }
        else if (shadowBeast.isBusy)
        {
            // ToDo appear animation
            shadowBeast.transform.GetChild(0).gameObject.SetActive(true);
            if (attackDelayCounter > 0)
            {
                attackDelayCounter -= Time.deltaTime;
            }
            else
            {
                // ToDo Attack animation
                foreach (var target in TargetsInRange())
                {
                    target.Health -= (int)damage;
                }

                shadowBeast.isBusy = false;
            }
        }
    }

    public void OnExit()
    {
        shadowBeast.isBusy = false;
    }

    private void SetupAttack()
    {
        shadowBeast.isBusy = true;
        // ToDo Disappear animation
        shadowBeast.transform.GetChild(0).gameObject.SetActive(false);
        hideTimeCounter = 1;
        attackDelayCounter = 0.5f;
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInRange = new List<CoreHealthHandler>();

        Vector2 centerPosition = shadowBeast.transform.position;

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
}
