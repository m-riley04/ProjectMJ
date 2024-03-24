using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ContractSettings
{
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DatePlayed { get; set; }
    public string Version { get; set; }

}

public class MainMenu : Menu
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
