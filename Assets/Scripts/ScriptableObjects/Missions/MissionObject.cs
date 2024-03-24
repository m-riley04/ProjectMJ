using JetBrains.Annotations;
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

[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission")]
public class MissionObject : ScriptableObject
{
    public string scene;
    [TextArea()] public string description;
    public Difficulty difficulty;

    [Header("World")]
    public Weather weather;

    [Header("Crash Site")]
    public CrashCause crashCause;
    public int artifactCount;
    public CraftType craftType;

    public void Init(string scene="Farmhouse", 
        string description="", 
        Difficulty difficulty=Difficulty.Medium, 
        Weather weather=Weather.None, 
        CraftType craftType=CraftType.TicTac, 
        CrashCause crashCause=CrashCause.Other, 
        int artifactCount=3)
    {
        this.scene          = scene;
        this.description    = description;
        this.difficulty     = difficulty;
        this.weather        = weather;
        this.craftType      = craftType;
        this.crashCause     = crashCause;
        this.artifactCount  = artifactCount;
    }
}
