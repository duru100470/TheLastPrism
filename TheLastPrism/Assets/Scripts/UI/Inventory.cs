using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;
    public int SelectedSlot {get; set;} = 0;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    public bool AcquireItem(ref Item _item)
    {
        bool isSuccessful = false;
        
        foreach (var slot in slots)
        {
            if (_item.Amount == 0) break;

            if (slot.item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
            }
            if (slot.item.ItemInfo.itemType == _item.ItemInfo.itemType && !slot.IsSlotFull())
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
            }
        }

        return isSuccessful;
    }

    public Item GetItemInfo(int _slot)
    {
        return slots[_slot].item;
    }

    public bool FindItem(ITEM_TYPE itemType, int amount)
    {
        int total = 0;

        foreach (var slot in slots)
        {
            if (slot.item.ItemInfo.itemType == itemType)
                total += slot.item.Amount;
        }

        return total >= amount;
    }

    public bool RemoveItem(int _slot)
    {
        bool isSuccessful = false;

        if (slots[_slot].item != null)
        {
            slots[_slot].ClearSlot();
            isSuccessful = true;
        }

        return isSuccessful;
    }
    
    public bool RemoveItem(ITEM_TYPE itemType, int amount)
    {
        bool isSuccessful = false;

        // foreach (var slot in slots)
        // {
            // if (slot.item.ItemType == itemType)
                // total += slot.item.Amount;
        // }

        return isSuccessful;
    }
}
