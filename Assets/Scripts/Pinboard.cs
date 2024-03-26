using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Pinboard : MonoBehaviour
{
    public int availableMissionCount = 5;
    public List<MissionData> missions;
    public List<string> availableScenes = new List<string>();
    public GameObject picturePrefab;

    float minWidth = 0.25f;
    float minHeight = 0.25f;
    float maxWidth = 2.5f;
    float maxHeight = 2.5f;
    float depth = -0.05f;

    // Start is called before the first frame update
    void Start()
    {
        // Create new missions
        for (int i = 0; i < availableMissionCount; i++)
        {
            missions.Add(GenerateRandomMission());
        }

        // Populate the corkboard with map buttons
        for (int i = 0; i < missions.Count; i++)
        {
            // Create the photo and populate data
            GameObject photo = Instantiate(picturePrefab);
            photo.GetComponentInChildren<PinboardPhoto>().mission = missions[i];

            // Randomize position
            float xPos = Random.Range(minWidth, maxWidth);
            float yPos = Random.Range(minHeight, maxHeight);
            Vector3 pos = new Vector3(xPos, yPos, depth);

            // Set the position
            photo.transform.SetParent(transform, false);
            photo.transform.position = transform.position + pos;

            // Check for other collisions
            int maxChecks = 5;
            for (int j = 0; j < maxChecks; j++)
            {
                if (photo.GetComponentInChildren<PinboardPhoto>().colliding)
                {
                    // Randomize position
                    xPos = Random.Range(minWidth, maxWidth);
                    yPos = Random.Range(minHeight, maxHeight);
                    pos = new Vector3(xPos, yPos, depth);

                    // Set the position
                    photo.transform.SetParent(transform, false);
                    photo.transform.position = transform.position + pos;
                }
                else break;
            }

        }

    }
    public MissionData GenerateRandomMission()
    {
        // Randomize each of the crash elements
        int sceneIndex = (int)Mathf.Round(Random.Range(0, availableScenes.Count - 1));
        string scene = availableScenes[sceneIndex];
        string description = ""; // TODO - Add description that matches the other settings.
        Difficulty difficulty = (Difficulty)Mathf.Round(Random.Range(0, 3));
        Weather weather = (Weather)Mathf.Round(Random.Range((int)Weather.None, (int)Weather.Other));
        CraftType craftType = (CraftType)Mathf.Round(Random.Range((int)CraftType.None, (int)CraftType.Other));
        CrashCause crashCause = (CrashCause)Mathf.Round(Random.Range((int)CrashCause.None, (int)CrashCause.Other));
        int artifactCount = (int)Mathf.Round(Random.Range(1, 4));

        // Create a new mission object
        MissionData mission = new MissionData(scene, description, (int)difficulty, (int)weather, (int)crashCause, artifactCount, (int)craftType);

        // Return it
        return mission;
    }
}
