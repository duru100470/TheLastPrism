using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block : Item
{
    [SerializeField]
    private BlockInfo blockInfo;
    public override ItemInfo ItemInfo => blockInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public Block(BlockInfo itemInfo, int amount)
    {
        this.blockInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new Block(blockInfo, amount);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
