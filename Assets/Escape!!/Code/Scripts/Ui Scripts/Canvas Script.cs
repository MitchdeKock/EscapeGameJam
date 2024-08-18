using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TMPro.TextMeshProUGUI flowCountText;
    [SerializeField] private TMPro.TextMeshProUGUI maxFlowText;
    // [SerializeField] private TMPro.TextMeshProUGUI DamagePriceText;
    // [SerializeField] private TMPro.TextMeshProUGUI MaxFlowPriceText;
    // [SerializeField] private TMPro.TextMeshProUGUI AttackRatePriceText;
    // [SerializeField] private TMPro.TextMeshProUGUI MovementSpeedPriceText;

    [Header("Button references")]
    [SerializeField] private Button upgrade_button_1;
    [SerializeField] private Button upgrade_button_2;
    [SerializeField] private Button upgrade_button_3;

    [Header("Prices")]
    [SerializeField] private int damagePrice = 10;
    [SerializeField] private int maxFlowPrice = 10;
    [SerializeField] private int attackRatePrice = 10;
    [SerializeField] private int movementSpeedPrice = 10;

    [Header("Player attacks")]
    [SerializeField] private AttackMeleeStaff mainAttack;
    [SerializeField] private AttackRangedStaff secondaryAttack;

    [SerializeField] private GameObject UpgradeScreen;
    
    private CoreHealthHandler coreScriptComponent;
    private PlayerController playerScript;


    void Start()
    {
        coreScriptComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        upgrade_button_1.onClick.AddListener(BuyUpgrade);
        upgrade_button_2.onClick.AddListener(BuyUpgrade);
        upgrade_button_3.onClick.AddListener(BuyUpgrade);
    }

    private void BuyUpgrade(int buttonNumber)
    {
        Debug.Log("cheese");
    }


    public void onDamageButtonClicked()
    {
        if (coreScriptComponent.Health > damagePrice)
        {
            coreScriptComponent.Health -= damagePrice;
            damagePrice += 5;
            mainAttack.Damage += 1;
            secondaryAttack.Damage += 1;
        }
    }

    public void onMaxFlowClicked()
    {
        if (coreScriptComponent.Health > maxFlowPrice)
        {
            coreScriptComponent.Health -= maxFlowPrice;
            maxFlowPrice += 5;
            coreScriptComponent.MaxHealth += 5;
        }
        else
        {
            //TODO something to show they cant buy the upgrade
        }
    }
    public void onAttackClicked()
    {
        if (coreScriptComponent.Health > attackRatePrice)
        {
            coreScriptComponent.Health -= attackRatePrice;
            mainAttack.Cooldown -= 0.01f;
            secondaryAttack.Cooldown -= 0.01f;
            attackRatePrice += 5;
        }
    }
    public void onMovementClicked()
    {
        if (coreScriptComponent.Health > movementSpeedPrice)
        {
            playerScript.MoveSpeed += 2;
            playerScript.DashSpeed += 2;
            coreScriptComponent.Health -= movementSpeedPrice;
            movementSpeedPrice += 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        flowCountText.text = "Flow: " + coreScriptComponent.Health.ToString();
        maxFlowText.text = "Max Flow: " + coreScriptComponent.MaxHealth.ToString();
       // DamagePriceText.text = damagePrice.ToString() + "F";
       // AttackRatePriceText.text = attackRatePrice.ToString() + "F";
       // MovementSpeedPriceText.text = movementSpeedPrice.ToString() + "F";
      //  MaxFlowPriceText.text = maxFlowPrice.ToString() + "F";


        if (Input.GetKeyDown(KeyCode.E))
        {
            PauseManager.TogglePause();
            UpgradeScreen.SetActive(!UpgradeScreen.activeSelf);
        }


    }
}
