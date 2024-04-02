using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<InventorySlotData> slots = new List<InventorySlotData>();
    public int size = 20;

    public InventoryData() { }
    public InventoryData(List<InventorySlotData> slots = null, int size = 20)
    {
        if (slots == null)
        {
            this.slots = slots;
        }
        this.size = size;
    }

    public void AddItem(ItemData _item, int _quantity)
    {
        // Check if we have the item in the inventory
        if (slots.Count >= size) throw new OverflowException("Inventory is full");

        // Check if we already have the item if it is stackable
        if (_item.stackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item == _item)
                {
                    slots[i].AddQuantity(_quantity);
                    return;
                }
            }
        }

        slots.Add(new InventorySlotData(_item, _quantity));
    }

    public void RemoveItemAt(int index)
    {
        slots.RemoveAt(index);
    }

    public InventorySlotData GetSlot(int index)
    {
        return slots[index];
    }
}

[System.Serializable]
public class InventorySlotData
{
    public ItemData item;
    public int quantity;

    public InventorySlotData() { }
    public InventorySlotData(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddQuantity(int value)
    {
        quantity += value;
    }
}