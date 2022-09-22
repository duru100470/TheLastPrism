using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRuleTile : RuleTile
{
    [Header("Gravity Tile")]
    [SerializeField]
    private GameObject tempTile;

    public override void UpdateRuleTile()
    {
        base.UpdateRuleTile();
        
        // Check there is a tile at the bottom
        if (Pos.Y == 0) return;
        Tile target = TileManager.Instance.TileArray[Pos.X, Pos.Y - 1];
        if (target != null) return;

        // Create temporary gravity tile
        GameObject newTile = Instantiate(tempTile);
        newTile.transform.position = new Vector2(Pos.X + 0.5f, Pos.Y + 0.5f);
        newTile.GetComponent<SpriteRenderer>().sprite = this.spriteRenderer.sprite;
        newTile.GetComponent<TempGravityRuleTile>().baseTile = this;
        
        // Destroy this tile
        TileManager.Instance.DestroyTile(Pos, false);
    }
}