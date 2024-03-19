using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    public float maxBattery;
    public bool on;

    private Light light;

    public void Awake()
    {
        //maxBattery = (EquipmentObject)item.maxBattery;
        light = GetComponentInChildren<Light>();
    }
    public void Toggle()
    {
        on = !on;
    }

    public void Update()
    {
        if (!on) light.intensity = 0f;
        else light.intensity = 2f;
    }
}
