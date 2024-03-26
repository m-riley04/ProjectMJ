using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class ItemData
{
    public int type;
    public string id;
    public string itemName;
    public string description;
    public bool stackable;

    public ItemData(int _type, string _id, string _itemName, string _description, bool _stackable)
    {
        this.type = _type;
        this.id = _id;
        this.itemName = _itemName;
        this.description = _description;
        this.stackable = _stackable;
    }
}
