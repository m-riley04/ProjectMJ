using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MissionObject mission;
    public Terrain terrain;
    public TerrainData terrainDataBackup;
    public GameObject crashPrefab;

    void Start()
    {
        // Get the mission
        //mission = FindFirstObjectByType<GameController>().currentMission.mission

        // Get the terrain
        if (!terrain) terrain = FindFirstObjectByType<Terrain>();
        terrainDataBackup = terrain.terrainData;
        Vector3 terrainSize = terrain.terrainData.size;

        // Set the crash location
        Random.seed = 0;
        int xPos = (int)Mathf.Round(Random.Range(0, terrainSize.x));
        float yPos = 5.57f;
        int zPos = (int)Mathf.Round(Random.Range(0, terrainSize.z));
        Vector3 pos = new Vector3(xPos, yPos, zPos);

        // Create crash location
        GameObject crashSite = Instantiate(crashPrefab, transform, false);
        crashSite.transform.position = pos;

        // Create hole at crash location
        int holeWidth = 40;
        int holeHeight = 40;
        int offSetZ = holeWidth/2;
        int offSetX = holeHeight/2;

        var b = new bool[holeWidth, holeHeight];

        Vector2 originOfCircle = new Vector2(offSetX, offSetZ);
        for (var x = 0; x < holeWidth; x++)
        {
            for (var y = 0; y < holeHeight; y++)
            {
                //b[x, y] = false;
                if (Vector2.Distance(new Vector2(x, y), originOfCircle) < offSetX)
                {
                    b[x, y] = false;
                }
                else
                {
                    b[x, y] = true;
                }
            }
        }


        terrain.terrainData.SetHoles(xPos, zPos, b);

        print("Position of Hole Start: " + xPos + ", " + zPos);
        print("Position of Crash Origin: " + pos);
    }
}
