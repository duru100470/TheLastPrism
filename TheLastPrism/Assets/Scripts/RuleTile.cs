using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleTile : Tile, IListener
{
    [SerializeField]
    private Sprite[] ruleTileSprites;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        EventManager.Instance.AddListener(EVENT_TYPE.AdjacentTileChange, this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        UpdateRuleTile();
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        switch(eType)
        {
            case EVENT_TYPE.AdjacentTileChange:
                // Coordinate coor = param as Coordinate;
                // if (Mathf.Abs(coor.X - Pos.X) > 1 || Mathf.Abs(coor.Y - Pos.Y) > 1)
                //     return;
                
                // UpdateRuleTile();
                break;
            default:
                break;
        }
    }
    
    public void UpdateRuleTile()
    {
        // Left Up
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Right Up
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[1];

        // Left Down
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[2];

        // Right Down
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[3];

        // Middle Up
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[4];

        // Right Middle
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[5];

        // Middle Down
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[6];

        // Left Middle
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[7];

        // Center
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[8];
    }

    private bool IsThereLeftTile()
    {
        if (Pos.X == 0) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X - 1, Pos.Y];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereRightTile()
    {
        if (Pos.X == TileManager.Instance.worldXSize - 1) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X + 1, Pos.Y];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereUpTile()
    {
        if (Pos.Y == TileManager.Instance.worldYSize - 1) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X, Pos.Y + 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereDownTile()
    {
        if (Pos.Y == 0) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X, Pos.Y - 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }
}