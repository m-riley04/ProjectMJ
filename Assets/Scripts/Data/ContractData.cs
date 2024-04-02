using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UniverseLore
{
    None,
    Simulation,
    Zoo,
    Other
}

[System.Serializable]
public class ContractData
{
    [Header("File Information")]
    public string name;
    public string version;
    public DateTime dateCreated;
    public DateTime dateLastPlayed;

    [Header("Lobby")]
    public List<MissionData> missions;
    public int money;
    public InventoryData storage;

    [Header("Players")]
    public PlayerData host;
    public List<PlayerData> players;

    [Header("Statistics")]
    public ContractStatisticsData stats;

    public ContractData()
    {
        missions    = new List<MissionData>();
        storage     = new InventoryData();
        host        = new PlayerData();
        players     = new List<PlayerData>();
        stats       = new ContractStatisticsData();
    }
}
