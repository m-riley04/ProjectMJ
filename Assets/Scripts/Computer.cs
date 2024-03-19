using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Computer : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas canvas;
    public GameObject canvasObject;
    public ScrollView scrollView;
    public TextMeshProUGUI uiInputText;
    public TextMeshProUGUI uiOutputText;

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

    private void Awake()
    {
        close();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            // Get the user input
            GetUserInput();

            // Set the input text
            uiInputText.text = currentInputText;
            uiOutputText.text = currentOutputText;
        }

    }

    public void open()
    {
        isOpen = true;
        currentOutputText = "Type 'help' to see a list of commands. \n";
        currentInputText = "";
        canvasObject.SetActive(true);
    }

    public void close()
    {
        isOpen = false;
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
