using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float maxBattery;
    public float on;
    public void Awake()
    {
        type = ItemType.Equipment;

        //OnUseEvent.AddListener();
    }
}
