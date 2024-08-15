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

        damageButton.onClick.AddListener(onDamageButtonClicked);
    }

    void onDamageButtonClicked()
    {
        playerScript.updateMoveSpeed();
    }

    void onMaxFlowClicked()
    {
        playerScript.updateMoveSpeed();
    }
    void onAttackClicked()
    {
        playerScript.updateMoveSpeed();
    }
    void onMovementClicked()
    {
        playerScript.updateMoveSpeed();
    }
    // Update is called once per frame
    void Update()
    {
        flowCountText.text = "Flow: " + coreScriptComponent.currentFlow.ToString();
        maxFlowText.text="Max Flow: "+ coreScriptComponent.maxFlow.ToString();
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
