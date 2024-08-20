using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CanvasScript : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TMPro.TextMeshProUGUI flowCountText;
    [SerializeField] private TMPro.TextMeshProUGUI maxFlowText;

    [Header("Button references")]
    [SerializeField] private Button upgrade_button_1;
    [SerializeField] private Button upgrade_button_2;
    [SerializeField] private Button upgrade_button_3;
    [SerializeField] private Button RefreshButton;


    [Header("Prices")]
    [SerializeField] private int damagePrice = 10;
    [SerializeField] private int maxFlowPrice = 10;
    [SerializeField] private int attackRatePrice = 10;
    [SerializeField] private int movementSpeedPrice = 10;


    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade rangedDamage;
    [SerializeField] private BaseUpgrade meleeDamage;
    [SerializeField] private BaseUpgrade movementSpeed;
    [SerializeField] private BaseUpgrade maxFlowUpgrade;
    [SerializeField] private BaseUpgrade meleeAttackRate;
    [SerializeField] private BaseUpgrade rangedAttackRate;
    [SerializeField] private BaseUpgrade dashSpeed;

    private List<BaseUpgrade> allUpgrades = new List<BaseUpgrade>();

    [Header("Rendering")]
    [SerializeField] private GameObject UpgradeScreen;
    
    private CoreHealthHandler coreScriptComponent;
    private PlayerController playerScript;


    void Start()
    {

        RefreshButton.onClick.AddListener(RefreshClicked);

        allUpgrades.Add(rangedDamage);
        allUpgrades.Add(meleeDamage);
        allUpgrades.Add(movementSpeed);
        allUpgrades.Add(maxFlowUpgrade);
        allUpgrades.Add(meleeAttackRate);
        allUpgrades.Add(rangedAttackRate);
        allUpgrades.Add(dashSpeed);

        coreScriptComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        assignUpgrades();

    }

    private void RefreshClicked()
    {
        if (coreScriptComponent.Health > 5)
        {
            assignUpgrades();
        }

    }
    private void assignUpgrades()
    {
        int min = 0;
        int max = allUpgrades.Count;
        int[] randomInts = Enumerable.Range(min, max).OrderBy(x => Random.Range(0, max)).Take(3).ToArray();

        BaseUpgrade upgrade1 = allUpgrades[randomInts[0]];
        BaseUpgrade upgrade2 = allUpgrades[randomInts[1]];
        BaseUpgrade upgrade3 = allUpgrades[randomInts[2]];

        SetUpgrade(upgrade1, upgrade_button_1);
        SetUpgrade(upgrade2, upgrade_button_2);
        SetUpgrade(upgrade3, upgrade_button_3);
    }


    private void SetUpgrade(BaseUpgrade upgrade, Button button)
    {
        button.transform.Find("Title_button").GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.name;
        button.transform.Find("Price_button").GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.price.ToString() + "F";
        button.transform.Find("Description_button").GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.description;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => BuyUpgrade(upgrade));
    }
    private void BuyUpgrade(BaseUpgrade upgrade)
    {
        if(coreScriptComponent.Health> upgrade.price)
        {
            upgrade.buyUpgrade();
            coreScriptComponent.Health -= upgrade.price;
            upgrade.price += 5;
            assignUpgrades();
        }
        else
        {
            Debug.Log("Sorry Cant Afford");
        }
    }


    // Update is called once per frame
    void Update()
    {
        flowCountText.text = "Flow: " + coreScriptComponent.Health.ToString();
        maxFlowText.text = "Max Flow: " + coreScriptComponent.MaxHealth.ToString();


        if (Input.GetKeyDown(KeyCode.E))
        {
            PauseManager.TogglePause();
            UpgradeScreen.SetActive(!UpgradeScreen.activeSelf);
        }


    }
}
