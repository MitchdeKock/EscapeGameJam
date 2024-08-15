using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealthUI : MonoBehaviour
{
    public Text text;
    public CoreHealthHandler _coreHealth;
    void Start()
    {
        text.text = $"Energy: {_coreHealth.getSouls()}";
        _coreHealth.OnValueChanged += UpdateScore;
    }

    void UpdateScore(int souls)
    {
        text.text = $"Energy: {souls}";
    }
}
