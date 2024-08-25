using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyState : IState
{
    private readonly HealthFlowBehaviour healthFlowBehaviour;

    public DestroyState(HealthFlowBehaviour healthFlowBehaviour)
    {
        this.healthFlowBehaviour = healthFlowBehaviour;
    }

    public void Tick() { }
    public void TickCooldown() { }

    public void OnEnter()
    {
        var coreHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        coreHealth.Health += healthFlowBehaviour.flowGiven;
        healthFlowBehaviour.totalFlow.Value += healthFlowBehaviour.flowGiven;
        GameObject.Destroy(healthFlowBehaviour.gameObject);
    }

    public void OnExit() { }
}