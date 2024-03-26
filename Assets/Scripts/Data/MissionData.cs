using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    MeteorShower,
    Other
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    TopSecret,
    Other
}

public enum CraftType
{
    None,
    Saucer,
    TicTac,
    Cigar,
    Sphere,
    Other
}

[System.Serializable]
public class MissionData
{
    public string scene;
    public string description;
    public int difficulty;
    public int weather;
    public int crashCause;
    public int artifactCount;
    public int craftType;

    public MissionData(string scene, string description, int difficulty, int weather, int crashCause, int artifactCount, int craftType)
    {
        this.scene          = scene;
        this.description    = description;
        this.difficulty     = difficulty;
        this.weather        = weather;
        this.crashCause     = crashCause;
        this.artifactCount  = artifactCount;
        this.craftType      = craftType;
    }
}
