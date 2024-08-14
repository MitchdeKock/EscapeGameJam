using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFlow : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasScript canvasScript;
    public Text flowMessage;
    void Start()
    {
        flowMessage.text="Flow: "+canvasScript.currentFlow.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
