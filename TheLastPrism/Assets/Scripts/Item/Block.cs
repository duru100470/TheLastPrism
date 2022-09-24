using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block : Item
{
    public Block(ItemInfo itemInfo, int amount)
    {
        this.itemInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new Block(itemInfo, amount);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
