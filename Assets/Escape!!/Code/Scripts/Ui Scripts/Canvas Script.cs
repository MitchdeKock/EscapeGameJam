using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    public int currentFlow;
    public int maxFlow;
    public TMPro.TextMeshProUGUI flowCount;
    public Button damageButton;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        currentFlow = 1;
        maxFlow = 10;
        canvas.enabled = false;
        damageButton.onClick.AddListener(onDamageButtonClicked);
    }

    void onDamageButtonClicked()
    {
        currentFlow++;
    }

    // Update is called once per frame
    void Update()
    {
        flowCount.text = "Flow: " + currentFlow.ToString();
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
