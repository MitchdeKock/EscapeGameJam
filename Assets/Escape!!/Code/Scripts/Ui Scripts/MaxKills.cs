using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxKills : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TMPro.TextMeshProUGUI TotalKillsText;

    [SerializeField] private FloatReference totalKills;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TotalKillsText.text = totalKills.Value.ToString();
    }
}
