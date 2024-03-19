using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum ShipType
{
    Saucer,
    TicTac,
    Triangle
}

public class CrashSite : MonoBehaviour
{
    [Range(0, 100)]
    public int playerDetectionRadius;

    [Header("Smoke")]
    bool smokeActive = true;
    [Range(0, 100)]
    public int smokeHeight;

    [Header("Item Spawning")]
    public List<GameObject> spawnLocations;
    public List<GameObject> possibleArtifacts;
    public List<GameObject> artifacts;

    [Header("Status")]
    public bool discovered = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Spawn artifacts
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            // Get the spawn location
            Transform pos = spawnLocations[i].transform;

            // Randomize the artifact
            int emptyChance = 3;
            int artifactIndex = (int)Mathf.Round(Random.Range(0, possibleArtifacts.Count + emptyChance));
            
            // Ensure no index errors
            if (artifactIndex >= possibleArtifacts.Count) continue;
            
            // Create game object
            GameObject artifact = Instantiate(possibleArtifacts[artifactIndex], pos);
            artifact.transform.parent = pos;

            // Append the artifact
            artifacts.Add(artifact);
        }

        // Determine artifact count
        int artifactCount = (int)Mathf.Round(Random.Range(1, spawnLocations.Count));
        print("Artifacts: " + artifacts.Count);
    }

    private void OnDrawGizmos()
    {
        // Draw the radius for player detection
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
