using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI flowCountText;
    public TMPro.TextMeshProUGUI maxFlowText;
    public TMPro.TextMeshProUGUI DamagePriceText;
    public TMPro.TextMeshProUGUI MaxFlowPriceText;
    public TMPro.TextMeshProUGUI AttackRatePriceText;
    public TMPro.TextMeshProUGUI MovementSpeedPriceText;

    public int damagePrice;
    public int maxFlowPrice;
    public int attackRatePrice;
    public int movementSpeedPrice;

    public Button damageButton;
    public Button maxFlowButton;
    public Button attackRateButton;
    public Button movementSpeedButton;

    public Canvas canvas;

    public GameObject Core;
    private coreScript coreScriptComponent;

    public GameObject Player;
    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;

        coreScriptComponent = Core.GetComponent<coreScript>();

        playerScript=Player.GetComponent<PlayerController>();

        if(Player == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            Debug.Log("NOT NULL");
        }

        damageButton.onClick.AddListener(onDamageButtonClicked);
        maxFlowButton.onClick.AddListener(onMaxFlowClicked);
        attackRateButton.onClick.AddListener(onAttackClicked);
        movementSpeedButton.onClick.AddListener(onMovementClicked);


        damagePrice = 10;
        attackRatePrice = 10;
        movementSpeedPrice = 10;
        maxFlowPrice = 10;
    }

    void onDamageButtonClicked()
    {
        if (coreScriptComponent.currentFlow > damagePrice)
        {
            coreScriptComponent.removeFlow(damagePrice);
            damagePrice += 5;

        }
    }

    void onMaxFlowClicked()
    {
        //   if (coreScriptComponent.currentFlow > maxFlowPrice)
        //   {
        coreScriptComponent.removeFlow(movementSpeedPrice);
        maxFlowPrice += 5;
        coreScriptComponent.upgradeMaxFlow();

        //  }
        //  else
        //  {
        //do something to show they cant buy the upgrade
        // }
    }
    void onAttackClicked()
    {
        playerScript.upgradeMovementSpeed();
    }
    void onMovementClicked()
    {
     playerScript.upgradeMovementSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        flowCountText.text = "Flow: " + coreScriptComponent.currentFlow.ToString();
        maxFlowText.text="Max Flow: "+ coreScriptComponent.maxFlow.ToString();
        DamagePriceText.text = damagePrice.ToString() + "F";
        AttackRatePriceText.text = attackRatePrice.ToString()+ "F";
        MovementSpeedPriceText.text = movementSpeedPrice.ToString()+ "F";
        MaxFlowPriceText.text=maxFlowPrice.ToString()+ "F";



        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.enabled = !canvas.enabled;
        }


    }
}
