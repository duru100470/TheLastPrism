using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemInfo", menuName = "Item Info")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ITEM_TYPE itemType;
    public Sprite itemSprite;
    public int maxStack;
}