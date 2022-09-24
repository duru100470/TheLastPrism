using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Material : Item
{
    public Material(ItemInfo itemInfo, int amount)
    {
        this.itemInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new Material(itemInfo, amount);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
