using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    public Player player;

    [Header("Missions")]
    public MissionObject currentMission;

    [Header("Settings")]
    [Header("Video")]
    [Range(0, 100)] public float postProcesingAmount = 100;
    [Range(30, 120)] public float fov           = 90;

    [Header("Sound")]
    [Range(0, 100)] public float volume         = 100;
    [Range(0, 100)] public float volumeSFX      = 100;
    [Range(0, 100)] public float volumeMusic    = 100;
    [Range(0, 100)] public float volumeBeings   = 100;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setVolume(float val)
    {
        volume = val;
    }
    public void setVolumeSFX(float val)
    {
        volumeSFX = val;
    }
    public void setVolumeMusic(float val)
    {
        volumeMusic = val;
    }
    public void setVolumeBeings(float val)
    {
        volumeBeings = val;
    }

    public void setPostProcess(float val)
    {
        postProcesingAmount = val;
    }
}
