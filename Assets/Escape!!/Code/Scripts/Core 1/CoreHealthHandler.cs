using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    public event Action<int> OnValueChanged;


    void Start()
    {
        health = 1;
        maxHealth = 20;
    }

    public void SetSouls(int souls)
    {
        health = Math.Min(souls, maxHealth);
        OnValueChanged?.Invoke(health);
    }

    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void ChangeHealth(int amountToAdd = 1)
    {
        SetSouls(health + amountToAdd);
    }

    public void RemoveHealth(int amountToRemove = 1)
    {
        Debug.Log($"{name} hit for {amountToRemove} damage");
        SetSouls(health - amountToRemove); ;
    }
    public void UpgradeMaxHealth(int amountToAdd = 5)
    {
        maxHealth += amountToAdd;
    }
}
