using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int _souls;
    [SerializeField] private int _maxSouls;

    public event Action<int> OnValueChanged;
    public void SetSouls(int souls)
    {
        _souls = Math.Min(souls, _maxSouls);
        OnValueChanged?.Invoke(_souls);
    }
    public int getSouls()
    {
        return _souls;
    }

    public void ChangeSouls(int amountToAdd = 1)
    {
        SetSouls(_souls + amountToAdd);
    }

    public void RemoveHealth(int amountToRemove = 1)
    {
        Debug.Log($"{name} hit for {amountToRemove} damage");
        SetSouls(_souls - amountToRemove); ;
    }
}
