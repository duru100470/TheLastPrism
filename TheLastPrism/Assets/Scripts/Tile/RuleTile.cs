using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleTile : Tile
{
    [SerializeField]
    private Sprite[] ruleTileSprites;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[8];

        // Left Up Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[9];
        
        // Right Up Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[10];

        // Right Down Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[11];

        // Left Down Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[12];

        // Left End
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[13];
        
        // Up End
        if (
            !IsThereLeftTile() &&
            !IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[14];

        // Right End
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            !IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[15];

        // Down End
        if (
            !IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[16];

        // Horizontal
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[17];

        // Vertical
        if (
            !IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[18];

        // Block
        if (
            !IsThereLeftTile() &&
            !IsThereRightTile() &&
            !IsThereUpTile() &&
            !IsThereDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[19];

        // LU RU Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[20];

        // RU RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[21];

        // LD RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[22];

        // LU LD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[23];

        // LU RU RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[24];

        // LD RU RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[25];

        // LU LD RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[26];

        // LU LD RU Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[27];

        // 4-Way Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[28];

        // LU Corner Down
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereRightUpTile()
        )
            spriteRenderer.sprite = ruleTileSprites[29];

        // RU Corner Down
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereRightUpTile()
        )
            spriteRenderer.sprite = ruleTileSprites[30];

        // RU Corner Left
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereRightUpTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[31];

        // RD Corner Left
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereRightUpTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[32];

        // RD Corner Up
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[33];

        // LD Corner Up
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[34];

        // LD Corner Right
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereLeftDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[35];

        // LU Corner Right
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereLeftDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[36];

        // LU RD Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            IsThereRightUpTile() && 
            IsThereLeftDownTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[37];

        // LD RU Corner
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            IsThereLeftUpTile() &&
            !IsThereRightUpTile() && 
            !IsThereLeftDownTile() && 
            IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[38];

        // Left Up RD
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[39];

        // Right Up LD
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[40];

        // Left Down RU
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile() &&
            !IsThereRightUpTile() 
        )
            spriteRenderer.sprite = ruleTileSprites[41];

        // Right Down LU
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile() &&
            !IsThereLeftUpTile()
        )
            spriteRenderer.sprite = ruleTileSprites[42];

        // LU RU Down
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            !IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereRightUpTile() 
        )
            spriteRenderer.sprite = ruleTileSprites[43];

        // RU RD Left
        if (
            !IsThereLeftTile() &&
            IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereRightUpTile() && 
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[44];

        // LD RD Up
        if (
            IsThereLeftTile() &&
            IsThereRightTile() &&
            !IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftDownTile() &&
            !IsThereRightDownTile()
        )
            spriteRenderer.sprite = ruleTileSprites[45];

        // LU LD Right
        if (
            IsThereLeftTile() &&
            !IsThereRightTile() &&
            IsThereUpTile() &&
            IsThereDownTile() &&
            !IsThereLeftUpTile() &&
            !IsThereLeftDownTile() 
        )
            spriteRenderer.sprite = ruleTileSprites[46];
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

    private bool IsThereLeftUpTile()
    {
        if (Pos.X == 0) return false;
        if (Pos.Y == TileManager.Instance.worldYSize - 1) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X - 1, Pos.Y + 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereRightUpTile()
    {
        if (Pos.X == TileManager.Instance.worldXSize - 1) return false;
        if (Pos.Y == TileManager.Instance.worldYSize - 1) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X + 1, Pos.Y + 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereLeftDownTile()
    {
        if (Pos.X == 0) return false;
        if (Pos.Y == 0) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X - 1, Pos.Y - 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }

    private bool IsThereRightDownTile()
    {
        if (Pos.X == TileManager.Instance.worldXSize - 1) return false;
        if (Pos.Y == 0) return false;
        Tile target = TileManager.Instance.TileArray[Pos.X + 1, Pos.Y - 1];
        if (target == null)
            return false;
        if (target.TileType == this.TileType)
            return true;
        else
            return false;
    }
}