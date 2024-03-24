using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinboardPhoto : MonoBehaviour
{
    public MissionObject mission;
    public bool colliding = false;

    private void Start()
    {
        // Set the photo name
        if (mission) GetComponentInChildren<TextMeshProUGUI>().text = mission.scene;
    }
}
