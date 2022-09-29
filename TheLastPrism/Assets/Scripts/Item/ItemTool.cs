using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTool : Item
{
    [SerializeField]
    private ToolInfo toolInfo;
    [SerializeField]
    private int durability;
    public override ItemInfo ItemInfo => toolInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public ItemTool(ToolInfo itemInfo, int amount, int durability)
    {
        this.toolInfo = itemInfo;
        this.amount = amount;
        this.durability = durability;
    }

    public override Item DeepCopy()
    {
        return new ItemTool(toolInfo, amount, durability);
    }

    public override void OnLeftClick()
    {
        // Check if entity is hit
        // Check if structure is hit

        // Check if tile is hit
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        Tile target = TileManager.Instance.TileArray[coor.X, coor.Y];

        if (target == null) return;
        target.GetDamage(toolInfo.damage, toolInfo.dmgType, 0, true);
        durability--;
        if (durability <= 0) Amount = 0;
    }

    public override void OnRightClick()
    {

    }
}
