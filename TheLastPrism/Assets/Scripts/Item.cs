using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private Sprite itemSprite;
    [SerializeField]
    private ITEM_TYPE itemType;
    [SerializeField]
    private int amount;
    [SerializeField]
    private int maxStack;
    
    public Sprite ItemSprite => itemSprite;
    public ITEM_TYPE ItemType => itemType;
    public int Amount
    {
        get { return amount; }
        set { amount = Mathf.Clamp(value, 0, maxStack); }
    }
    public int MaxStack => maxStack;

    public Item (Item prevItem)
    {
        this.itemSprite = prevItem.ItemSprite;
        this.itemType = prevItem.ItemType;
        this.amount = prevItem.Amount;
        this.maxStack = prevItem.MaxStack;
    }
}