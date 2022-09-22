using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textCount;

    public Item item { set; get; }

    private void Start()
    {
        ClearSlot();
    }

    private void SetColor(float _alpha)
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }

    private void SetCount(int _count)
    {
        textCount.text = _count.ToString();
        if (_count <= 1)
            textCount.alpha = 0f;
        else
            textCount.alpha = 1f;
    }

    public void AddItem(ref Item _item)
    {
        if (item == null)
        {
            item = new Item(_item);
            _item.Amount = 0;
        }
        // item.ItemType == _item.ItemType
        else
        {
            int tmp = item.ItemInfo.maxStack - item.Amount;
            item.Amount += _item.Amount;
            _item.Amount -= tmp;
        }

        image.sprite = item.ItemInfo.itemSprite;
        SetColor(1f);
        SetCount(item.Amount);
    }

    public void ClearSlot()
    {
        item = null;
        image.sprite = null;
        SetColor(0);
        SetCount(0);
    }

    public bool IsSlotFull()
    {
        return item.Amount == item.ItemInfo.maxStack;
    }
}
