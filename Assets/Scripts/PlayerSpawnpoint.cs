using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject gameControllerPrefab;

    void Start()
    {
        // Check if gamecontroller already exists
        if (!FindObjectOfType<GameController>())
        {
            // If it doesn't, create it
            GameObject gameController = Instantiate(gameControllerPrefab, transform, false);
            gameController.transform.parent = null;
            DontDestroyOnLoad(gameController);
        }

        // Check if player already exists
        if (!FindObjectOfType<Player>())
        {
            // If it doesn't, create it
            GameObject player = Instantiate(playerPrefab, transform, false);
            player.transform.parent = null;
            DontDestroyOnLoad(player);
            GameController.playerLoaded = true;
        }
    }
}
