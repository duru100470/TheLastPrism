using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(ref Item _item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(_item);
                _item.Amount = 0;
            }
            if (slot.item.ItemType == _item.ItemType && !slot.IsSlotFull())
            {
                slot.AddItem(_item);
            }
        }
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
}
