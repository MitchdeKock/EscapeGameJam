using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Movement Speed Upgrade", menuName = "Upgrades/MovementSpeed")]
public class upgradeMovementSpeed : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    public override int currentUpgrades
    {
        get => upgradeAmount;
        set => upgradeAmount = value;
    }

    public override int maxUpgrade => maxUpgrades;

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Movement Speed";
    [SerializeField] private string UpgradeDescription = "Increases movement speed";
    [SerializeField] private int UpgradePrice = 10;
    [SerializeField] private int maxUpgrades = 10;
    [SerializeField] private int upgradeAmount = 0;

    private PlayerController playerScript;

    private void OnEnable()
    {
        price = 10;
        upgradeAmount = 0;

    }

    public override void ResetUpgrade()
    {
        price = 10;
        upgradeAmount = 0;
    }
    public override void buyUpgrade()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerScript.MoveSpeed += 2;
    }
}
