using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreScript : MonoBehaviour
{
    public int currentFlow;
    public int maxFlow;
    // Start is called before the first frame update
    void Start()
    {
        currentFlow = 1;
        maxFlow = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFlow()
    {
        if (currentFlow < maxFlow)
        {
            currentFlow++;
        }
    }
}
