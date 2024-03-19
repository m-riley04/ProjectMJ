using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Artifact Object", menuName = "Inventory/Items/Artifact")]
public class ArtifactObject : ItemObject
{
    public float value;
    public void Awake()
    {
        type = ItemType.Artifact;

        //OnUseEvent.AddListener();
    }
}
