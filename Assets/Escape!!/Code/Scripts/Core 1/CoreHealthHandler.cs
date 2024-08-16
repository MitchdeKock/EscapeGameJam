using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int maxHealth = 20;

    public int Health 
    { 
        get => health;
        set
        {
            health = Math.Min(value, maxHealth);
            OnValueChanged?.Invoke(health);
        }
    }

    public int MaxHealth 
    { 
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }

    public event Action<int> OnValueChanged;
}
