using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VineAmbushState : IState
{
    private float ambushCooldown;
    private float ambushRange;
    private float ambushDamage;
    private ToxicVineBehaviour vine;
    private CoreHealthHandler target;

    private float tryAmbushCounter;
    private float ambushCooldownCounter;
    private float warningCounter;
    private float ambushPrepareCounter;
    private Phase phase;
    public VineAmbushState(float ambushCooldown, float ambushRange, float ambushDamage, ToxicVineBehaviour vine, CoreHealthHandler target)
    {
        this.ambushCooldown = ambushCooldown;
        this.ambushRange = ambushRange;
        this.ambushDamage = ambushDamage;
        this.vine = vine;
        this.target = target;
    }

    public void OnEnter()
    {
        Debug.Log($"{vine.name} has entered {this.GetType().Name}");
        // todo add idle animation
        vine.isBusy = false;
        phase = Phase.idle;
        tryAmbushCounter = 1;
        ambushCooldownCounter = ambushCooldown;
        warningCounter = 1;
    }

    public void Tick()
    {
        if (tryAmbushCounter > 0 && !vine.isBusy)
        {
            tryAmbushCounter -= Time.deltaTime;
        }
        else
        {
            if (UnityEngine.Random.Range(1, 100) >= 20 && ambushCooldownCounter <= 0) 
            {
                // ToDo ammbush
                vine.isBusy = true;
                ambushCooldownCounter = ambushCooldown;

                ambushPrepareCounter = 1;
                phase = Phase.prepareAttack;
                HideVine();
            }
            tryAmbushCounter = 1;
        }

        switch (phase)
        {
            case Phase.idle:
                if (ambushCooldownCounter > 0)
                {
                    ambushCooldownCounter -= Time.deltaTime;
                }
                break;
            case Phase.prepareAttack:
                if (ambushPrepareCounter > 0)
                {
                    ambushPrepareCounter -= Time.deltaTime;
                }
                else
                {
                    Vector3 targetPosition = target.transform.position;
                    targetPosition.z = vine.transform.position.z;
                    vine.transform.position = targetPosition;
                    ShowWarning();
                    phase = Phase.initiateAttack;
                }
                break;
            case Phase.initiateAttack:
                if (warningCounter > 0)
                {
                    warningCounter -= Time.deltaTime;
                }
                else
                {
                    Ambush();
                }
                break;
            default:
                break;
        }


    }

    public void OnExit()
    {
        vine.isBusy = false;
    }

    private void Ambush()
    {
        HideWarning();
        ShowVine();

        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)ambushDamage;
        }

        phase = Phase.idle;
        vine.isBusy = false;
    }

    private void ShowWarning()
    {
        vine.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void HideWarning()
    {
        vine.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void HideVine()
    {
        vine.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void ShowVine()
    {
        vine.transform.GetChild(0).gameObject.SetActive(true);
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = vine.transform.position;

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
        idle,
        prepareAttack,
        initiateAttack
    }
}
