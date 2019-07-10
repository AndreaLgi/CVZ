using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wonder : MonoBehaviour
{
    public string wonderName;
    public int wonderAtkBonus;
    public int wonderDefBonus;
    public Manager manag;
    public int panelNumber;

    public void ToggleWonder()
    {
        Manager.wonder togWonder;
        togWonder.atkBonus = wonderAtkBonus;
        togWonder.defBonus = wonderDefBonus;
        togWonder.name = wonderName;
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            manag.playersInGame[panelNumber].AddWonder(togWonder);
        }
        else
        {
            manag.playersInGame[panelNumber].RemoveWonder(togWonder);
        }
    }
}
