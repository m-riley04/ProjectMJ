using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour, IInteractable
{
    public InteractableObject interactable;

    public void Interact()
    {
        print("Interacted with interactable");
        SceneManager.LoadScene(interactable.transitionScene);
    }
}
