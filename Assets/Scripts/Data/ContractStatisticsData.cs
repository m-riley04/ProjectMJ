using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContractStatisticsData
{
    public int missionsCompleted;
    public int missionsFailed;
    public int artifactsDiscovered;
    public int craftsDiscovered;
    public int entitiesDiscovered;

    public ContractStatisticsData(int missionsCompleted, int missionsFailed, int artifactsDiscovered, int craftsDiscovered, int entitiesDiscovered)
    {
        this.missionsCompleted = missionsCompleted;
        this.missionsFailed = missionsFailed;
        this.artifactsDiscovered = artifactsDiscovered;
        this.craftsDiscovered = craftsDiscovered;
        this.entitiesDiscovered = entitiesDiscovered;
    }
}
