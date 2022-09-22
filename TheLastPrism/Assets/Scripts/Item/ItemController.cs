using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isAcquirable = false;
    public Item item;

    public bool IsAcquirable => isAcquirable;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite()
    {
        spriteRenderer.sprite = item.ItemInfo.itemSprite;
    }

    public void SetAcquirable(float time)
    {
        StartCoroutine(DelaySetAcquirable(time));
    }

    private IEnumerator DelaySetAcquirable(float time)
    {
        yield return new WaitForSeconds(time);
        isAcquirable = true;
    }
}
