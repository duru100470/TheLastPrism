using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : Item
{
    [SerializeField]
    private ToolInfo toolInfo;
    [SerializeField]
    private int durability;
    public override ItemInfo ItemInfo => toolInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public Tool(ToolInfo itemInfo, int amount, int durability)
    {
        this.toolInfo = itemInfo;
        this.amount = amount;
        this.durability = durability;
    }

    public override Item DeepCopy()
    {
        return new Tool(toolInfo, amount, durability);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
