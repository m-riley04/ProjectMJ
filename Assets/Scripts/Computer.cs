using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Computer : MonoBehaviour, IInteractable
{
    [Header("UI Elements")]
    public Canvas canvas;
    public GameObject canvasObject;
    public ScrollView scrollView;
    public TextMeshProUGUI uiInputText;
    public TextMeshProUGUI uiOutputText;
    public Player player;

    List<string> commands = new List<string>
    {
        "help",
        "close",
        "exit",
        "quit"
    };
    string currentInputText = "";
    string currentOutputText = "";

    public bool isOpen = false;

    private void Start()
    {
        close();
        SceneManager.sceneLoaded += OnLoadCallback;
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Try to get player
        if (!player)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        if (isOpen)
        {
            // Get the user input
            GetUserInput();

            // Set the input text
            uiInputText.text = currentInputText;
            uiOutputText.text = currentOutputText;
        }

    }

    public void Interact()
    {
        open();
    }

    public void open()
    {
        print("Computer opened");
        isOpen = true;
        if (player)
        {
            player.canMove = false;
            player.inUi = true;
        }
        currentOutputText = "Type 'help' to see a list of commands. \n";
        currentInputText = "";
        canvasObject.SetActive(true);
    }

    public void close()
    {
        isOpen = false;
        if (player)
        {
            player.canMove = true;
            player.inUi = false;
        }
        canvasObject.SetActive(false);
    }

    void GetUserInput()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (currentInputText.Length != 0)
                {
                    currentInputText = currentInputText.Substring(0, currentInputText.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                string command = currentInputText;
                printToCommandLine(currentInputText); // Add the command to the output history
                currentInputText = ""; // Reset the input text
                HandleCommandSubmit(command); // Handle the typed command
            }
            else
            {
                currentInputText += c;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Escape pressed
        {
            // Close the command line
            close();
        }
    }

    void HandleCommandSubmit(string command)
    {
        print("Command Inputted: " + command);
        switch (command)
        {
            case ("help"):
                help();
                break;
            case ("close" or "exit" or "quit"):
                close();
                break;
            default:
                printToCommandLine("ERROR: Command '" + command + "' not found. Please type 'help' for a list of commands.");
                break;
        }
        
    }

    void printToCommandLine(string str, string end="\n")
    {
        currentOutputText += str + end;
    }

    void help()
    {
        printToCommandLine("=== List of Commands ===");
        foreach (string command in commands)
        {
            printToCommandLine("- " + command);
        }
    }
}
