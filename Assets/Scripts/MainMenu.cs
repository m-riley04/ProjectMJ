using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ContractSettings
{
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DatePlayed { get; set; }
    public string Version { get; set; }

}

public class MainMenu : MonoBehaviour
{
    void NavigateToNextPage()
    {

    }

    void StartNewGame(ContractSettings settings)
    {

    }

    void QuitGame()
    {
        Application.Quit();
    }
}
