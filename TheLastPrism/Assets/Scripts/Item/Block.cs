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
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        TileManager.Instance.TileArray[coor.X, coor.Y]?.GetDamage(1, 0, true);
    }

    public override void OnRightClick()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        if (TileManager.Instance.PlaceTile(coor, blockInfo.tileType))
            Amount--;
    }
}
