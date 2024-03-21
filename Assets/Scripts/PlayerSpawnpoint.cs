using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        // Check if player already exists
        if (!FindObjectOfType<Player>())
        {
            GameObject player = Instantiate(playerPrefab, transform, false);
            player.transform.parent = null;
            DontDestroyOnLoad(player);
        }
    }
}
