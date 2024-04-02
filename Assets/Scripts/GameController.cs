using System;
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
    public ItemDatabaseObject itemDatabase;

    [Header("Contract")]
    public static ContractData contract = new();

    [Header("Player")]
    public static Player player;
    public static bool playerLoaded = false;

    [Header("Missions")]
    public static MissionData mission;

    [Header("Overall Game")]
    public static SettingsData settings;

    [Header("Contracts UI")]
    public GameObject contractPrefab;
    public RectTransform contractsContent;
    public Button contractStartButton;
    public Button contractDeleteButton;
    public Button selectedContractButton;
    public TMP_InputField nameField;
    public TMP_InputField seedField;
    public string selectedContractPath;

    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.root);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case ("MainMenu"):
                // Populate the contracts scroll view with all the user's save files
                PopulateContracts();
                break;
            case ("Lobby"):
                // Save the game when the lobby is loaded
                Save();
                break;
        }
    }

    private void Update()
    {
        if (playerLoaded && player)
        {
            print("Player has been created. Reloading data...");
            Load(Application.dataPath + "/SaveFiles/" + contract.name);
            playerLoaded = false;
        }
    }

    public void Save()
    {
        string subfolder = Application.dataPath + "/SaveFiles/" + contract.name;

        // Check if save path doesn't exist
        if (!Directory.Exists(subfolder))
        {
            Directory.CreateDirectory(subfolder);
        }

        //=== Contract
        string contractPath = subfolder + "/contract" + ".json";

        // Save the contract
        SaveLoadSystem.Save(contract, contractPath);
        print("Saved contract.");

        //=== Inventory
        string inventoryPath = subfolder + "/inventory" + ".json";
        if (player)
        {
            // Save the contract
            SaveLoadSystem.Save(player.inventory, inventoryPath);
            print("Saved inventory.");
        }
    }

    public void Load(string path)
    {
        string subfolder = path;

        // Check if the save folder doesn't exist
        if (!Directory.Exists(subfolder))
        {
            Directory.CreateDirectory(subfolder);
        }

        //== Contract
        string contractPath = subfolder + "/contract" + ".json";

        // Check if the file doesn't exist
        if (!File.Exists(contractPath))
        {
            throw new System.Exception("Contract does not exist");
        }

        // Load the contract
        contract = SaveLoadSystem.Load<ContractData>(contractPath);
        print("Loaded contract.");

        //== Inventory
        string inventoryPath = subfolder + "/inventory" + ".json";

        // Check if the file doesn't exist
        if (!File.Exists(inventoryPath))
        {
            throw new System.Exception("Inventory data does not exist");
        }

        // Load the inventory
        if (player)
        {
            player.inventory = SaveLoadSystem.Load<InventoryData>(inventoryPath);

            // Iterate inventory items
            for (int i = 0; i < player.inventory.slots.Count; i++)
            {
                // Check the ID
                int id = player.inventory.GetSlot(i).item.id;
                if (id == itemDatabase.items[id].id)
                {
                    player.inventory.GetSlot(i).item.sprite = itemDatabase.items[id].sprite;
                    player.inventory.GetSlot(i).item.prefab = itemDatabase.items[id].prefab;
                }
            }

            print("Loaded inventory.");
        }
    }

    public void PopulateContracts()
    {
        // Get the number of saves
        string contractsPath = Application.dataPath + "/SaveFiles";
        string[] contracts = Directory.GetDirectories(contractsPath);

        // Iterate through each save
        for (int i = 0; i < contracts.Length; i++)
        {
            // Create a contract ui element under the contracts scroll view content
            GameObject _contractButton = Instantiate(contractPrefab, contractsContent.transform, false);

            // Set the button's visuals
            _contractButton.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileName(contracts[i]);

            // Set the button's callback
            int i2 = i;
            _contractButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                if (selectedContractButton) selectedContractButton.image.color = Color.white;
                selectedContractPath = contracts[i2];
                contractStartButton.interactable = true;
                contractDeleteButton.interactable = true;
                selectedContractButton = _contractButton.GetComponentInChildren<Button>();
                selectedContractButton.image.color = Color.blue;
                contract.name = Path.GetFileName(contracts[i2]);
                print("Selected save '" + contracts[i2] + "'");
            });

            // Add to it's position
            _contractButton.transform.position += new Vector3(0, -100 - (i * 100), 0);
        }
    }

    public void StartButtonPressed()
    {
        // Load the contract
        Load(selectedContractPath);

        // Change the scene
        SceneManager.LoadScene(1);
    }

    public void DeleteButtonPressed()
    {
        // Delete the directory
        Directory.Delete(selectedContractPath, true);

        // Remove all buttons
        for (int i = 0; i < contractsContent.childCount; i++) 
        {
            Destroy(contractsContent.GetChild(i).gameObject);
        }

        // Re-populate
        PopulateContracts();

        // Set the contract buttons
        contractStartButton.interactable = false;
        contractDeleteButton.interactable = false;
        selectedContractButton = null;
    }

    public void StartNewContract()
    {
        ContractData _contract = new ContractData();

        // Get the text from the name field
        _contract.name = nameField.text;

        // Get the text from the seed field

        // Set the current contract
        contract = _contract;

        print(_contract.name);

        // Load the lobby
        SceneManager.LoadScene(1);
    }
}
