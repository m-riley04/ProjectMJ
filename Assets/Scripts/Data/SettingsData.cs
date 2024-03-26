using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    [Header("Settings")]
    [Header("Video")]
    [Range(0, 100)] public float postProcesingAmount = 100;
    [Range(30, 120)] public float fov = 90;

    [Header("Sound")]
    [Range(0, 100)] public float volume = 100;
    [Range(0, 100)] public float volumeSFX = 100;
    [Range(0, 100)] public float volumeMusic = 100;
    [Range(0, 100)] public float volumeBeings = 100;
}
