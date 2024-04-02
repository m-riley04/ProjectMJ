using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class Item : MonoBehaviour
{
    public ItemData item;
    public ItemObject itemTemplate;
    public Light itemLight;
    public Camera itemCamera;
    public int FileCounter = 0;

    [Header("Sounds")]
    public AudioClip soundPickup;
    public AudioClip soundDrop;
    public AudioClip soundRun;
    public AudioClip soundActivate;
    public AudioClip soundDeactivate;

    public void Start()
    {
        // Set the actual item data from the item template
        item = new ItemData((int)itemTemplate.type, itemTemplate.id, itemTemplate.itemName, itemTemplate.description, itemTemplate.stackable, itemTemplate.sprite, itemTemplate.prefab);

        if (itemLight)
        {
            itemLight.enabled = false;
        }
    }

    public void OnPickup()
    {
        
    }

    public void OnDrop()
    {

    }

    public void OnFlashlightToggle()
    {
        if (itemLight) {
            itemLight.enabled = !itemLight.enabled;
        } else { print("Light not found on item."); }
    }
    public void OnCameraCapture()
    {
        if (itemCamera)
        {
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = itemCamera.targetTexture;

            itemCamera.Render();

            Texture2D Image = new Texture2D(itemCamera.targetTexture.width, itemCamera.targetTexture.height);
            Image.ReadPixels(new Rect(0, 0, itemCamera.targetTexture.width, itemCamera.targetTexture.height), 0, 0);
            Image.Apply();
            RenderTexture.active = currentRT;

            var Bytes = Image.EncodeToPNG();
            Destroy(Image);

            // Check if directory doens't exist
            if (!Directory.Exists(Application.dataPath + "/CameraPictures"))
            {
                // Create it
                Directory.CreateDirectory(Application.dataPath + "/CameraPictures");
            }

            File.WriteAllBytes(Application.dataPath + "/CameraPictures/" + FileCounter + ".png", Bytes);
            FileCounter++;
        } else
        {
            print("Camera not found on item");
        }
    }

}
