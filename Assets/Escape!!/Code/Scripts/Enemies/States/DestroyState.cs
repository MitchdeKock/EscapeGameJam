using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyState : IState
{
    private readonly MonoBehaviour _monoBehaviour;

    public DestroyState(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    public void Tick() { }

    public void OnEnter()
    {
        GameObject.Destroy(_monoBehaviour.gameObject);
    }

    public void OnExit() { }
}