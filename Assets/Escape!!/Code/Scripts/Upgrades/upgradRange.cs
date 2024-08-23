using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Upgrade", menuName = "Upgrades/Ranged")]
public class upgradRange : BaseUpgrade
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
    [SerializeField] private string UpgradeName = "Projectile Range";
    [SerializeField] private string UpgradeDescription = "Increases Ranged on Projectile";
    [SerializeField] private int UpgradePrice = 10;
    [SerializeField] private int maxUpgrades = 10;
    [SerializeField] private int upgradeAmount = 0;


    [Header("Dependencies")]
    [SerializeField] private AttackRangedStaff secondaryAttack;
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
        secondaryAttack.Range += 1;
    }
}
