using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    CrashRetrieval,
    EntityContainment,
    Landing
}

public enum CrashCause
{
    None,
    Weather,
    Weaponry,
    ElectricalFailure,
    PurposfulLanding,
    Other
}

public enum Weather
{
    None,
    Rain,
    Thunderstorm,
    Foggy,

}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    TopSecret
}

public enum EntityRace
{
    None,
    Greys,
    Light,
    Europan
}

[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission")]
public class MissionObject : ScriptableObject
{
    public string scene;
    public new string name;
    public Difficulty difficulty;

    [Header("World")]
    public Weather weather;

    [Header("Crash Site")]
    public CrashCause crashCause;
    public int artifactCount;

    [Header("Entities")]
    public EntityRace entityRace;
    public int entityCount;
}
