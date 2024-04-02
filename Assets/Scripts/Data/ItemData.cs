using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int type;
    public int id;
    public string itemName;
    public string description;
    public bool stackable;
    [JsonIgnore] public Sprite sprite;
    [JsonIgnore] public GameObject prefab;

    public ItemData() { }
    public ItemData(ItemData item)
    {
        this.type = item.type;
        this.id = item.id;
        this.itemName = item.itemName;
        this.description = item.description;
        this.stackable = item.stackable;
        this.sprite = item.sprite;
        this.prefab = item.prefab;
    }
    public ItemData(int type, int id, string itemName, string description, bool stackable, Sprite sprite, GameObject prefab)
    {
        this.type = type;
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.stackable = stackable;
        this.sprite = sprite;
        this.prefab = prefab;
    }
    
}
