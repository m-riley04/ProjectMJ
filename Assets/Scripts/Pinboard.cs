using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Pinboard : MonoBehaviour
{
    public List<string> missions;
    public GameObject picturePrefab;

    // Start is called before the first frame update
    void Start()
    {

        
        // Populate the corkboard with map buttons
        for (int i = 0; i < missions.Count; i++)
        {
            GameObject photo = Instantiate(picturePrefab);
            photo.transform.SetParent(transform, false);
            photo.transform.position = transform.position + new Vector3(0f, 2 + (-0.6f * i), -0.05f);
            photo.GetComponentInChildren<TextMeshProUGUI>().text = missions[i];
            /*
            string sceneName = missions[i];
            GameObject button = Instantiate<GameObject>(buttonPrefab);
            Vector3 pos = ui.transform.position + new Vector3(0, -0.25f * i, 0);
            button.transform.SetParent(ui.transform.gameObject.transform, false);
            button.transform.position = pos;
            button.transform.localScale = new Vector3(1, 1, 1);
            button.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
            button.GetComponent<Button>().onClick.AddListener(() => HandleMissionButtonClicked(sceneName));
            */
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleMissionButtonClicked(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
