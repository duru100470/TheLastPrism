using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : Item
{
    [SerializeField]
    private int durability;

    public Tool(ItemInfo itemInfo, int amount, int durability)
    {
        this.itemInfo = itemInfo;
        this.amount = amount;
        this.durability = durability;
    }

    public override Item DeepCopy()
    {
        return new Tool(itemInfo, amount, durability);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
