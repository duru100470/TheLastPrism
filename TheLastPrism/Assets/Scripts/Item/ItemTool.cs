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

    public ItemTool(ToolInfo itemInfo, int amount)
    {
        this.toolInfo = itemInfo;
        this.amount = amount;
        this.durability = itemInfo.maxDurability;
    }

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
        Collider2D[] entityColls = GameManager.Instance.CurPlayer.GetAttackedCollider2D(LayerMask.GetMask("Enemy"));
        if (entityColls.Length > 0)
        {
            foreach (var coll in entityColls)
            {
                coll.GetComponent<Structure>().GetDamage(toolInfo.damage, toolInfo.dmgType, 0, true);
            }

            UseDurability();
            return;
        }

        // Check if structure is hit
        Collider2D[] structureColls = GameManager.Instance.CurPlayer.GetAttackedCollider2D(LayerMask.GetMask("Structure"));
        if (structureColls.Length > 0)
        {
            foreach (var coll in structureColls)
            {
                coll.GetComponent<Structure>().GetDamage(toolInfo.damage, toolInfo.dmgType, 0, true);
            }

            UseDurability();
            return;
        }

        // Check if tile is hit
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        Tile target = TileManager.Instance.TileArray[coor.X, coor.Y];

        if (target == null) return;
        target.GetDamage(toolInfo.damage, toolInfo.dmgType, 0, true);
        UseDurability();
    }

    public override void OnRightClick()
    {

    }

    private void UseDurability()
    {
        durability--;
        if (durability <= 0) Amount = 0;
    }
}
