using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Damage Upgrade", menuName = "Upgrades/MeleeDamage")]
public class upgradeMeleeDamage : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Melee Damage";
    [SerializeField] private string UpgradeDescription = "Increases melee Damage";
    [SerializeField] private int UpgradePrice = 10;

    [Header("Dependencies")]
    [SerializeField] private AttackMeleeStaff mainAttack;

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
      
        mainAttack.Damage += 1;
    }
}
