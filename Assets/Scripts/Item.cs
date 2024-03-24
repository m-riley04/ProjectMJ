using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public Light itemLight;
    public Camera itemCamera;
    public int FileCounter = 0;

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
