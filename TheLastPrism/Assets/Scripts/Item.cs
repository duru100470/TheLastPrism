using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    private ItemInfo itemInfo;
    [SerializeField]
    private int amount;
    
    public ItemInfo ItemInfo => itemInfo;
    public int Amount
    {
        get { return amount; }
        set { amount = Mathf.Clamp(value, 0, itemInfo.maxStack); }
    }

    public Item (Item prevItem)
    {
        this.itemInfo = prevItem.ItemInfo;
        this.amount = prevItem.Amount;
    }
}