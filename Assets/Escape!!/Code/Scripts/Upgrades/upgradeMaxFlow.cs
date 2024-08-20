using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New maxFlow Upgrade", menuName = "Upgrades/maxFlow")]
public class upgradeMaxFlow : BaseUpgrade
{
    public override string name => UpgradeName;
    public override string description => UpgradeDescription;
    public override int price
    {
        get => UpgradePrice;
        set => UpgradePrice = value;
    }

    [Header("Stats")]
    [SerializeField] private string UpgradeName = "Max Flow";
    [SerializeField] private string UpgradeDescription = "Increases Max Flow";
    [SerializeField] private int UpgradePrice = 10;

    [Header("Dependencies")]
    private CoreHealthHandler coreScriptComponent;
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
        coreScriptComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        coreScriptComponent.MaxHealth += 5;
    }
}
