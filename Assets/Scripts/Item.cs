using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public Light itemLight;

    public void Start()
    {
        // Make the item a copy
        item = Instantiate(item);

        if (itemLight)
        {
            itemLight.enabled = false;
        }
    }

    public void OnFlashlightToggle()
    {
        if (itemLight) {
            itemLight.enabled = !itemLight.enabled;
        }
    }

}
