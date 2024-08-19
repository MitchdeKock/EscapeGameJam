using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeMovementSpeed : BaseUpgrade
{

    [Header("Stats")]
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int price;

    public override void buyUpgrade(int Money)
    {
        if (Money > price)
        {
            price += 5;
        }
    }
}
