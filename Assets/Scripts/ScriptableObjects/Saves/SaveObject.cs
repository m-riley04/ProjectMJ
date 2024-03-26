using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    public new string name;
    public string path;
    //public List<PlayerData> playersData;

    [Header("Lobby")]
    public List<MissionObject> availableMissions;

    [Header("World Options")]
    public UniverseLore lore;

    [Header("Statistics")]
    public int missionsCompleted;
    public int missionsFailed;
    public int artifactsDiscovered;
    public int craftsDiscovered;
    public int entitiesDiscovered;
}
