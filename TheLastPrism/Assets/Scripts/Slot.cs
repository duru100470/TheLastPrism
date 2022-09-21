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

    public void AddItem(Item _item)
    {
        item = new Item(_item);
        image.sprite = item.ItemSprite;
        SetColor(1f);
        SetCount(item.Amount);
    }

    public void SetSlotCount(int _count)
    {
        item.Amount += _count;
        SetCount(item.Amount);

        if (item.Amount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        item = null;
        SetColor(0);
        SetCount(0);
    }

    public bool IsSlotFull()
    {
        return item.Amount == item.MaxStack;
    }
}
