using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dash Speed Upgrade", menuName = "Upgrades/DashSpeed")]
public class upgradeDashSpeed : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Dash Speed";
    [SerializeField] private string UpgradeDescription = "Increases Dash speed";
    [SerializeField] private int UpgradePrice = 10;

    private PlayerController playerScript;

    private void OnEnable()
    {
        price = 10;
    }

    public override void ResetUpgrade()
    {
        price = 10;
    }
    public override void buyUpgrade()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerScript.DashSpeed += 2;
    }
}
