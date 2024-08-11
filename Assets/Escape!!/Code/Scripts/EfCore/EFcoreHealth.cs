using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class efcore : MonoBehaviour
{
    [SerializeField] private float _souls;

    public event Action<float> OnValueChanged;
    public void SetSouls(float souls)
    {
        _souls = souls;
        OnValueChanged?.Invoke(_souls);
    }
    public float getSouls()
    {
        return _souls;
    }

   public void AddSoul()
    {
        SetSouls(_souls+1);
        Debug.Log(_souls);
    }
    public void RemoveSoul()
    {
        SetSouls(_souls - 1);
    }
}
