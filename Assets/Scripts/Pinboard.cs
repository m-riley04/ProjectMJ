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
    public List<string> missions;
    public GameObject picturePrefab;

    float minWidth = 0.25f;
    float minHeight = 0.25f;
    float maxWidth = 2.5f;
    float maxHeight = 2.5f;
    float depth = -0.05f;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the corkboard with map buttons
        for (int i = 0; i < missions.Count; i++)
        {
            // Create the photo and populate data
            GameObject photo = Instantiate(picturePrefab);
            photo.GetComponentInChildren<TextMeshProUGUI>().text = missions[i];

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
}
