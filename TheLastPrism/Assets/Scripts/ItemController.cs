using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Item item;
    public Item Item
    {
        get {return item;}
        set {item = value; spriteRenderer.sprite = Item.ItemInfo.itemSprite;}
    }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
