using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[CreateAssetMenu(fileName = "New Hotbar", menuName = "Inventory/Hotbar")]
public class HotbarObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int maxSize = 4;

    public void AddItem(ItemObject _item, int _quantity)
    {
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