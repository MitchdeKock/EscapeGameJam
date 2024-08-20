using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Attack Ratge Upgrade", menuName = "Upgrades/RangedAttackRate")]
public class upgradeRangedAttackRate : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Ranged Attack rate";
    [SerializeField] private string UpgradeDescription = "Increases Ranged Attack Rate";
    [SerializeField] private int UpgradePrice = 10;

    [Header("Dependencies")]
    [SerializeField] private AttackRangedStaff secondaryAttack;
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
        secondaryAttack.Cooldown -= 0.01f;
    }
}
