using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBlock : Item
{
    [SerializeField]
    private BlockInfo blockInfo;
    public override ItemInfo ItemInfo => blockInfo;
    public override int Amount { get => amount; set => amount = Mathf.Clamp(value, 0, ItemInfo.maxStack); }

    public ItemBlock(BlockInfo itemInfo, int amount)
    {
        this.blockInfo = itemInfo;
        this.amount = amount;
    }

    public override Item DeepCopy()
    {
        return new ItemBlock(blockInfo, amount);
    }

    public override void OnLeftClick()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        TileManager.Instance.TileArray[coor.X, coor.Y]?.GetDamage(1, DAMAGE_TYPE.Hand, 0, true);
    }

    public override void OnRightClick()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Coordinate coor = Coordinate.WorldPointToCoordinate(point);

        if (Coordinate.Distance(Coordinate.WorldPointToCoordinate(GameManager.Instance.CurPlayer.transform.position), coor) > 2) return;

        Tile[,] tileArray = TileManager.Instance.TileArray;
        if ((coor.X < TileManager.Instance.worldXSize && tileArray[coor.X + 1, coor.Y] != null) ||
            (coor.X > 0 && tileArray[coor.X - 1, coor.Y] != null) ||
            (coor.Y < TileManager.Instance.worldYSize && tileArray[coor.X, coor.Y + 1] != null) ||
            (coor.Y > 0 && tileArray[coor.X, coor.Y - 1] != null)
        )
        {
            if (TileManager.Instance.PlaceTile(coor, blockInfo.tileId))
                Amount--;
        }
    }
}
