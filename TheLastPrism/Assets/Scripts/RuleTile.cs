using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleTile : Tile, IListener
{
    [SerializeField]
    private Sprite[] ruleTileSprites;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        EventManager.Instance.AddListener(EVENT_TYPE.TileChange, this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        switch(eType)
        {
            case EVENT_TYPE.TileChange:
                break;
            default:
                break;
        }
    }
    
    private void UpdateRuleTile()
    {
        // Left Up
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Right Up
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Left Down
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Right Down
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Middle Up
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Right Middle
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Middle Down
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Left Middle
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];

        // Center
        if (
            TileManager.Instance.GetLeftTile(x, y)?.tileType == this.tileType 
            && TileManager.Instance.GetRightTile(x, y)?.tileType != this.tileType
            && TileManager.Instance.GetUpTile(x, y)?.tileType == this.tileType
            && TileManager.Instance.GetDownTile(x, y)?.tileType != this.tileType
        )
            spriteRenderer.sprite = ruleTileSprites[0];
    }
}