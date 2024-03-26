using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Contract")]
    public ContractData contract;

    [Header("Player")]
    public Player player;

    [Header("Missions")]
    public MissionData mission;

    [Header("Overall Game")]
    public SettingsData settings;

    [Header("Create Contract")]
    public TMP_InputField nameField;
    public TMP_InputField seedField;


    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.root);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case ("Lobby"):
                // Save the game when the lobby is loaded
                Save();
                break;
        }
    }

    public void Save()
    {
        string subfolder = Application.dataPath + "/SaveFiles/" + contract.name;
        string path = subfolder + "/contract" + ".json";

        // Check if path doesn't exist
        if (!Directory.Exists(subfolder))
        {
            Directory.CreateDirectory(subfolder);
        }

        SaveLoadSystem.Save(contract, path);
        print("Saved contract.");
    }

    public void Load()
    {
        string subfolder = Application.dataPath + "/SaveFiles/" + contract.name;
        string path = subfolder + "/contract" + ".json";

        // Check if path doesn't exist
        if (!Directory.Exists(subfolder))
        {
            Directory.CreateDirectory(subfolder);
        }

        // Check if the file doesn't exist
        if (!File.Exists(path))
        {
            throw new System.Exception("Contract does not exist");
        }

        contract = SaveLoadSystem.Load<ContractData>(path);
        print("Loaded contract.");
    }

    public void StartNewContract()
    {
        ContractData _contract = new ContractData();

        // Get the text from the name field
        _contract.name = nameField.text;

        // Get the text from the seed field

        // Set the current contract
        this.contract = _contract;

        print(_contract.name);

        // Load the lobby
        SceneManager.LoadScene(1);
    }
}
