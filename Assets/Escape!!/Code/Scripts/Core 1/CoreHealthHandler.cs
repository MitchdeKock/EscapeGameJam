using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int TotalCollected = 0;

    public int TotalHealth { get => TotalCollected; }
    public int Health 
    { 
        get => health;
        set
        {
            int difference = Math.Max(Math.Min(value, maxHealth) - Health,0);
            TotalCollected += difference;
            health = Math.Min(value, maxHealth);
            OnHealthValueChanged?.Invoke(health);
        }
    }

    public int MaxHealth 
    { 
        get => maxHealth;
        set
        {
            maxHealth = value;
            OnMaxHealthValueChanged?.Invoke(maxHealth);
        }
    }

    public event Action<int> OnHealthValueChanged;
    public event Action<int> OnMaxHealthValueChanged;

}
