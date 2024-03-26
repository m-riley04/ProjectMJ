using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MissionData mission;
    public Terrain terrain;
    public RenderTexture backupTexture;
    public GameObject crashPrefab;
    public GameController gameController;
    public Vector3 crashPosition;

    void Start()
    {
        // Get the terrain
        if (!terrain) terrain = FindFirstObjectByType<Terrain>();

        // Get the game controller
        gameController = FindFirstObjectByType<GameController>();

        // Get the mission
        if (gameController) mission = gameController.mission;

        // Place the crash site
        if (mission != null && terrain) PlaceCrashSite(terrain.terrainData); else print("Unable to place crash site.");
        
    }

    private void Update()
    {
        if (!gameController)
        {
            // Find the game controller
            gameController = FindObjectOfType<GameController>();

            // If it's found...
            if (gameController)
            {
                // Set the mission
                mission = gameController.mission;

                // Place the crash site
                if (mission != null && terrain) PlaceCrashSite(terrain.terrainData);
            }
        }
    }

    public void PlaceCrashSite(TerrainData terrainData)
    {
        // Get the bounds
        Vector3 terrainSize = terrainData.size;

        // Set the crash location
        float xPos = Mathf.Round(Random.Range(0, terrainSize.x));
        float yPos = 5.57f;
        float zPos = Mathf.Round(Random.Range(0, terrainSize.z));
        Vector3 pos = new Vector3(xPos, yPos, zPos);
        crashPosition = pos; // Set the crash position
        print("Crash Position: " + pos);

        // Create crash location
        GameObject crashSite = Instantiate(crashPrefab, transform, false);
        crashSite.transform.position = pos;

        // Create dynamic holes
        int holeWidth = 40;
        int holeHeight = 40;
        CreateDynamicHoles(xPos, zPos, holeWidth, holeHeight);

    }

    public void CreateDynamicHoles(float xPos, float zPos, int width, int height)
    {
        // Create hole at crash location
        int offSetZ = width / 2;
        int offSetX = height / 2;

        var b = new bool[width, height];

        Vector2 originOfCircle = new Vector2(offSetX, offSetZ);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
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

        terrain.terrainData.SetHoles((int)xPos, (int)zPos, b);
    }

    public void RemoveDynamicHoles(TerrainData terrainData, float xPos, float zPos, int width, int height)
    {
        var b = new bool[width, height];

        // Iterate through every hole position
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                // Set it to true
                b[x, y] = true;
            }
        }

        // Set the hole values
        terrainData.SetHoles((int)xPos, (int)zPos, b);
    }

    private void OnApplicationQuit()
    {
        // Fill the holes from the crash site
        RemoveDynamicHoles(terrain.terrainData, crashPosition.x, crashPosition.z, 40, 40);
    }
}
