using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    Artifact,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public ItemType type;
    public string id;
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public Sprite image;
    public GameObject prefab;
    public bool stackable;
}
