using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int maxSize = 20;

    public void AddItem(ItemObject _item, int _quantity)
    {
        // Check if we have the item in the inventory
        if (Container.Count >= maxSize) throw new OverflowException("Hotbar is full");

        // Check if we already have the item if it is stackable
        if (_item.stackable)
        {
            for (int i = 0; i < Container.Count; i++)
            {
                if (Container[i].item == _item)
                {
                    Container[i].AddQuantity(_quantity);
                    return;
                }
            }
        }

        Container.Add(new InventorySlot(_item, _quantity));
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quantity;
    public InventorySlot(ItemObject _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }

    public void AddQuantity(int value)
    {
        quantity += value;
    }
}
