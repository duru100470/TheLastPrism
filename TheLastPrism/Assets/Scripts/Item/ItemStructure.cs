using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStructure : Item
{
    [SerializeField]
    private StructureInfo structureInfo;
    public override ItemInfo ItemInfo => structureInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public ItemStructure(StructureInfo itemInfo, int amount)
    {
        this.structureInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new ItemStructure(structureInfo, amount);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
