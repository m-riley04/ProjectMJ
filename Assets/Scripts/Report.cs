using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReportMenu : Menu
{
    [Header("Observables")]
    [SerializeField] Toggle toggleAntiGrav;
    [SerializeField] Toggle toggleAcceleration;
    [SerializeField] Toggle toggleHypersonic;
    [SerializeField] Toggle toggleCloaking;
    [SerializeField] Toggle toggleTransMedium;
    [SerializeField] Toggle toggleBiological;

    [Header("Characteristics")]

    [Header("Signatures")]
    [SerializeField] Toggle toggleAcoustic;
    [SerializeField] Toggle toggleRadio;
    [SerializeField] Toggle toggleLight;
    [SerializeField] Toggle toggleThermal;
    [SerializeField] Toggle toggleGravitational;
    [SerializeField] Toggle toggleRadiation;
    [SerializeField] Toggle toggleOtherSignatures;

    [Header("Crash Reasoning")]
    [SerializeField] Toggle toggleElectrical;
    [SerializeField] Toggle toggleWeather;
    [SerializeField] Toggle toggleDamage;
    [SerializeField] Toggle toggleOtherReason;

    [Header("Intelligence")]
    [SerializeField] Toggle toggleIntelligent;
    [SerializeField] Toggle toggleHuman;
    [SerializeField] Toggle toggleNHI;
    [SerializeField] Toggle toggleRecovered;
    [SerializeField] Toggle toggleAlive;

    private void Update()
    {
        // Check for next page button
        if (Input.GetKeyDown(KeyCode.Period)) { pages[1].gameObject.SetActive(true); pages[0].gameObject.SetActive(false); }
        if (Input.GetKeyDown(KeyCode.Comma)) { pages[1].gameObject.SetActive(false); pages[0].gameObject.SetActive(true); }
    }

    void NextPage()
    {
        if (currentPage+1 < pages.Count)
        {
            currentPage++;
        }
    }

    void PreviousPage()
    {
        if (currentPage-1 >= 0)
        {
            currentPage--;
        }
    }

}
