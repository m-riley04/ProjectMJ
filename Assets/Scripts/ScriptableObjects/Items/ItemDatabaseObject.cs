using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]
public class ItemDatabaseObject : ScriptableObject
{
    public List<ItemObject> items = new List<ItemObject>();
}
