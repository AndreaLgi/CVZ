using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiploToggle : MonoBehaviour
{
    public int playerDiplo;
    public int panelNumber;
    public Manager mainManag;
    public Toggle atkTog;
    public Toggle defTog;

    public void Start()
    {
        mainManag = GameObject.Find("EventSystem").GetComponent<Manager>();

    }

    public void AtkToggle()
    {
        if (atkTog.isOn)
        {
            mainManag.playersInGame[panelNumber].pDiploAtk[playerDiplo] = 2;
        }
        else
        {
            mainManag.playersInGame[panelNumber].pDiploAtk[playerDiplo] = 0;
        }
    }

    public void DefToggle()
    {
        if (defTog.isOn)
        {
            mainManag.playersInGame[panelNumber].pDiploDef[playerDiplo] = 2;
        }
        else
        {
            mainManag.playersInGame[panelNumber].pDiploDef[playerDiplo] = 0;
        }
    }
}
