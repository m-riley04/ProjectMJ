using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public InteractableObject interactable;

    public void OnInteract()
    {
        print("Interacted with interactable");
        SceneManager.LoadScene(interactable.transitionScene);
    }
}
