using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public efcore _efcore;
    void Start()
    {
        textMeshProUGUI.text = $"Energy: {_efcore.getSouls()}";
        _efcore.OnValueChanged += UpdateScore;
    }

    void UpdateScore(float souls)
    {
        textMeshProUGUI.text = $"Energy: {souls}";
    }
}
