using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Material : Item
{
    [SerializeField]
    private MaterialInfo materialInfo;
    public override ItemInfo ItemInfo => materialInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public Material(MaterialInfo itemInfo, int amount)
    {
        this.materialInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new Material(materialInfo, amount);
    }

    public override void OnLeftClick()
    {

    }

    public override void OnRightClick()
    {

    }
}
