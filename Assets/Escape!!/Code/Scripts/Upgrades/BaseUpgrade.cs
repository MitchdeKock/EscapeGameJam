using System;
using UnityEngine;

public abstract class BaseUpgrade : ScriptableObject
{
    public abstract string name { get; }
    public abstract string description { get; }
    public abstract int price { get; set; }
    public abstract int maxUpgrade { get; }
    public abstract int currentUpgrades { get;set; }
    public abstract void buyUpgrade();
    public abstract void ResetUpgrade();
}
