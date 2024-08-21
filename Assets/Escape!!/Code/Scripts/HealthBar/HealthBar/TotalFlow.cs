using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalFlow : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI flowCountText;

    [Header("Character Dependency")]
    private CoreHealthHandler coreScriptComponent;
    // Start is called before the first frame update
    void Start()
    {
        coreScriptComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        flowCountText.text=coreScriptComponent.TotalHealth.ToString();
    }
}
