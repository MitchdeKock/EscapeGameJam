using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Damage Upgrade", menuName = "Upgrades/RangedDamage")]
public class upgradeRangedDamage : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Ranged Damage";
    [SerializeField] private string UpgradeDescription = "Increases Ranged Damage";
    [SerializeField] private int UpgradePrice = 10;

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

            Debug.Log(UpgradeName);
    }
}
