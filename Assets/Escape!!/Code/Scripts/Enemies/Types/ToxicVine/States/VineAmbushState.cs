using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VineAmbushState : IState
{
    private float ambushCooldown;
    private float ambushRange;
    private float ambushDamage;
    private ToxicVineBehaviour vineBehaviour;
    private CoreHealthHandler target;
    private HealthBar healthBar;

    private float attackCooldownCounter;
    private float tryAmbushCounter;
    private float warningCounter;
    private float underGroundCounter;
    private Phase phase;
    public VineAmbushState(float ambushCooldown, float ambushRange, float ambushDamage, ToxicVineBehaviour vineBehaviour, CoreHealthHandler target)
    {
        this.ambushCooldown = ambushCooldown;
        this.ambushRange = ambushRange;
        this.ambushDamage = ambushDamage;
        this.vineBehaviour = vineBehaviour;
        this.target = target;
        healthBar = vineBehaviour.GetComponent<HealthBar>();
    }

    public void OnEnter()
    {
        if (vineBehaviour.ShowDebug)
            Debug.Log($"{vineBehaviour.name} has entered {this.GetType().Name}");

        vineBehaviour.isBusy = false;
        phase = Phase.Idle;
        tryAmbushCounter = 1;
    }

    public void Tick()
    {
        if (tryAmbushCounter > 0 && !vineBehaviour.isBusy)
        {
            tryAmbushCounter -= Time.deltaTime;
        }
        else
        {
            if (attackCooldownCounter <= 0) 
            {
                if (UnityEngine.Random.Range(1, 100) >= 20)
                {
                    // ToDo ammbush
                    vineBehaviour.isBusy = true;
                    attackCooldownCounter = ambushCooldown;

                    underGroundCounter = 1;
                    phase = Phase.UnderGround;
                    HideVine();
                }
            }
            tryAmbushCounter = 1;
        }

        switch (phase)
        {
            case Phase.UnderGround:
                if (underGroundCounter > 0)
                {
                    underGroundCounter -= Time.deltaTime;
                }
                else
                {
                    Vector3 targetPosition = target.transform.position;
                    targetPosition.z = vineBehaviour.transform.position.z;
                    vineBehaviour.transform.position = targetPosition;
                    ShowWarning();
                    phase = Phase.InitiateAttack;
                    warningCounter = 1;
                }
                break;
            case Phase.InitiateAttack:
                if (warningCounter > 0)
                {
                    warningCounter -= Time.deltaTime;
                }
                else
                {
                    Attack();
                }
                break;
            default:
                break;
        }
    }

    public void TickCooldown()
    {
        if (attackCooldownCounter > 0 && phase == Phase.Idle)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
    }

    public void OnExit()
    {
        vineBehaviour.isBusy = false;
    }

    private void Attack()
    {
        HideWarning();
        ShowVine();

        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)ambushDamage;
        }

        phase = Phase.Idle;
        vineBehaviour.isBusy = false;
    }

    private void ShowWarning()
    {
        vineBehaviour.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void HideWarning()
    {
        vineBehaviour.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void HideVine()
    {
        vineBehaviour.transform.GetChild(0).gameObject.SetActive(false);
        healthBar.HideHealthBar();
    }

    private void ShowVine()
    {
        vineBehaviour.transform.GetChild(0).gameObject.SetActive(true);
        healthBar.UnHideHealthBar();
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = vineBehaviour.transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, ambushRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<CoreHealthHandler>(out CoreHealthHandler playerHealth))
            {
                objectsInArc.Add(playerHealth);
            }
        }

        return objectsInArc;
    }

    private enum Phase
    {
        Idle,
        UnderGround,
        InitiateAttack
    }
}
