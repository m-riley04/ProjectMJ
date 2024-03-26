using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinboardPhoto : MonoBehaviour
{
    public MissionData mission;
    public bool colliding = false;

    private void Start()
    {
        // Set the photo name
        if (mission != null) GetComponentInChildren<TextMeshProUGUI>().text = mission.scene;
    }
}
