using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    MainMenu,
    Lobby,
    Mission
}

public class GameController : MonoBehaviour
{
    public Mission currentMission;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Lobby");
    }
}
