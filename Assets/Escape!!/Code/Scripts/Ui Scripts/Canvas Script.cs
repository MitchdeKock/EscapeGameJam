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

    public GameObject UpgradeScreen;

    public GameObject Core;
    private CoreHealthHandler coreScriptComponent;

    public GameObject Player;
    private PlayerController playerScript;

    [SerializeField] private BaseWeapon mainAttack;
    [SerializeField] private BaseWeapon secondaryAttack;
    // Start is called before the first frame update
    void Start()
    {

        UpgradeScreen.SetActive(false);

        coreScriptComponent = Core.GetComponent<CoreHealthHandler>();

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
        if (coreScriptComponent.getHealth() > damagePrice)
        {
            coreScriptComponent.RemoveHealth(damagePrice);
            damagePrice += 5;
            mainAttack.Damage += 1;
            secondaryAttack.Damage += 1;
        }
    }

    void onMaxFlowClicked()
    {
           if (coreScriptComponent.getHealth() > maxFlowPrice)
           {
                coreScriptComponent.RemoveHealth(movementSpeedPrice);
                maxFlowPrice += 5;
                coreScriptComponent.UpgradeMaxHealth();

          }
          else
         {
            //TODO something to show they cant buy the upgrade
         }
    }
    void onAttackClicked()
    {
        if(coreScriptComponent.getHealth()> attackRatePrice)
        {
            coreScriptComponent.RemoveHealth(attackRatePrice);
            mainAttack.Cooldown -= 0.01f;
            secondaryAttack.Cooldown -= 0.01f;
            attackRatePrice += 5;
        }
    }
    void onMovementClicked()
    {
        if (coreScriptComponent.getHealth()> movementSpeedPrice)
        {
            playerScript.upgradeMovementSpeed();
            coreScriptComponent.RemoveHealth(movementSpeedPrice);
            movementSpeedPrice += 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        flowCountText.text = "Flow: " + coreScriptComponent.getHealth().ToString();
        maxFlowText.text="Max Flow: "+ coreScriptComponent.getMaxHealth().ToString();
        DamagePriceText.text = damagePrice.ToString() + "F";
        AttackRatePriceText.text = attackRatePrice.ToString()+ "F";
        MovementSpeedPriceText.text = movementSpeedPrice.ToString()+ "F";
        MaxFlowPriceText.text=maxFlowPrice.ToString()+ "F";


        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeScreen.SetActive(!UpgradeScreen.activeSelf);
        }


    }
}
