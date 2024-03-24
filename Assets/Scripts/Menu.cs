using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public int currentPage = 0;
    public List<Canvas> pages;
    public void NavigateToPage(int page)
    {
        // Make the current page inactive
        pages[currentPage].gameObject.SetActive(false);

        // Update current page number
        currentPage = page;

        // Make the new page active
        pages[currentPage].gameObject.SetActive(true);
    }

    private void Awake()
    {
        // Turn off each gameobject
        foreach (Canvas page in pages)
        {
            page.gameObject.SetActive(false);
        }

        // Only enable the one that's being shown
        pages[currentPage].gameObject.SetActive(true);
    }
}
