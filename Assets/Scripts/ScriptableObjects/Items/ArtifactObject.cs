using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ArtifactCondition
{
    None,
    Unrecoverable,
    Ruined,
    Damaged,
    Fair,
    Undamaged,
    Good,
    Great,
    Excellent,
    Pristine
}

[CreateAssetMenu(fileName = "New Artifact Object", menuName = "Inventory/Items/Artifact")]
public class ArtifactObject : ItemObject
{
    public ArtifactCondition condition;
    public float value;
    public void Awake()
    {
        type = ItemType.Artifact;
    }
}
