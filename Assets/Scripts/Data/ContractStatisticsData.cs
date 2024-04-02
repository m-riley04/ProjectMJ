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

    public ContractStatisticsData(int missionsCompleted=0, int missionsFailed=0, int artifactsDiscovered = 0, int craftsDiscovered = 0, int entitiesDiscovered = 0)
    {
        this.missionsCompleted = missionsCompleted;
        this.missionsFailed = missionsFailed;
        this.artifactsDiscovered = artifactsDiscovered;
        this.craftsDiscovered = craftsDiscovered;
        this.entitiesDiscovered = entitiesDiscovered;
    }
}
