using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<Toggle> playersToggle;
    public List<GameObject> playersPanel;
    public List<GameObject> bigPanel;
    public List<GameObject> dropdownChar;
    public List<GameObject> dropdownTech;
    public GameObject diplomacyToggle;
    public GameObject attackPanel;
    public GameObject atkPnl;
    public GameObject defPnl;
    public GameObject attackPrefab;
    public List<Toggle> kabulToggle;
    public List <Toggle> cartagineToggle;
    public int activePlayersNum=0;
    public int attacker, defender;
    public List<technology> defaultTech = new List<technology>();
    public List<Sprite> charImages;

    //oggetti pannello attacco
    public GameObject atkChar, defChar;
    public Toggle atkCityToggle;
    public Text terrainAtkText, terrainDefText;
    public Dropdown terrainDropdown;
    public int terrainAtkValue=0, terrainDefValue=1;
    public Text atkWonderText, defWonderText;
    public int atkWonderValue=0, defWonderValue=0;
    public Text slotText;
    public Dropdown slotDropdown;
    public Text rinfTokenText;
    public Dropdown rinfTokenDropdown;
    public Text kabulAtkText;
    public int kabulAtkValue=0;
    public Text cartagineAtkText, cartagineDefText;
    public Dropdown cartagAtkDropdown, cartagDefDropdown;
    public int cartagAtkValue, cartagDefValue;
    public Text allyAtkText, allyDefText;
    public int allyAtkValue=0, allyDefValue=0;
    public Text tokenAtkText, tokenDefText;
    public int tokenAtkValue=0, tokenDefValue=0;
    public Text inputAtk, inputDef;
    public Text totalAtkText, totalDefText;
    public int totalAtkValue, totalDefValue;

    public void UpdateTerrain()
    {
        terrainAtkValue = 0;
        terrainDefValue = terrainDropdown.value + 1;
        if (atkCityToggle.isOn)
        {
            terrainDefValue *= 2;
        }
        if (playersInGame[attacker].pChar == 2)
        {
            if (terrainDropdown.value < 2)
            {
                terrainAtkValue = 3;
            }
        }
        if (playersInGame[defender].pChar == 2) 
        {
            if (terrainDropdown.value < 2)
            {
                terrainDefValue += 3;
            }
        }
        terrainDefText.text = "+" + terrainDefValue;
        terrainAtkText.text = "+" + terrainAtkValue;
        UpdateTotal();
    }

    public void UpdateSlot()
    {
        slotText.text = "+" + (slotDropdown.value + 1 + playersInGame[attacker].pTech.techBonus);
        UpdateTotal();
    }

    public void UpdateRinf()
    {
        rinfTokenText.text = "+" + rinfTokenDropdown.value;
        UpdateTotal();
    }

    public void UpdateKabul()
    {
        kabulAtkValue = 0;
        if ((atkCityToggle.isOn) && (playersInGame[attacker].kabul))
        {
            kabulAtkValue = 3;
        }
        kabulAtkText.text = "+" + kabulAtkValue;
        UpdateTotal();
    }

    public void UpdateCartagDef()
    {
        cartagDefValue=0;
        if (playersInGame[defender].cartagine) 
        {
            cartagDefValue = cartagDefDropdown.value;           
        }
        cartagineDefText.text = "+" + cartagDefValue;
        UpdateTotal();
    }

    public void UpdateCartagAtk()
    {
        if (playersInGame[attacker].cartagine)
        {
            cartagAtkValue = cartagAtkDropdown.value;
            cartagineAtkText.text = "+" + cartagAtkValue;
        }
        UpdateTotal();
    }


    public void UpdateTotal()
    {
        totalAtkValue = terrainAtkValue + atkWonderValue + slotDropdown.value + 1 + playersInGame[attacker].pTech.techBonus + kabulAtkValue + cartagAtkValue + allyAtkValue + tokenAtkValue;
        totalDefValue = terrainDefValue + defWonderValue + rinfTokenDropdown.value + cartagDefValue + allyDefValue + tokenDefValue;
        totalAtkText.text = "" + totalAtkValue;
        totalDefText.text = "" + totalDefValue;
    }

    private void Start()
    {
        technology tempTech;
        //nessun bonus
        tempTech.techBonus = 0;
        tempTech.barb = 0;
        defaultTech.Add(tempTech);
        //+1 in combattimento (+3 se contro i barbari)
        tempTech.techBonus = 1;
        tempTech.barb = 2;
        defaultTech.Add(tempTech);
        //+2 in combattimento
        tempTech.techBonus = 2;
        tempTech.barb = 0;
        defaultTech.Add(tempTech);
        //+3 in combattimento
        tempTech.techBonus = 3;
        tempTech.barb = 0;
        defaultTech.Add(tempTech);
    }

    public struct character
    {
        public Sprite charImg;
    };

    public struct technology
    {
        public int techBonus;
        public sbyte barb;
    }

    public struct wonder
    {
        public string name;
        public int atkBonus;
        public int defBonus;
    }

    public class Player
    {
        public Color pColor;
        public int pChar=0;
        public technology pTech;
        public List<wonder> pWonder = new List<wonder>();
        public List<int> pDiploAtk = new List<int>();
        public List<int> pDiploDef = new List<int>();
        public bool cartagine = false;
        public bool kabul = false;


        public Player(Color ownColor,technology ownTech)
        {
            pColor = ownColor;
            pTech = ownTech;
            for(int i=0; i<4; i++)
            {
                pDiploAtk.Add(0);
                pDiploDef.Add(0);
            }
        }


        public void AddWonder(wonder ownWonder)
        {
            bool alreadyOwned=false;
            foreach (wonder i in pWonder)
                if (i.name == ownWonder.name)
                    alreadyOwned = true;
            if (!alreadyOwned)
                pWonder.Add(ownWonder);
        }

        public void RemoveWonder(wonder remWonder)
        {
            pWonder.Remove(remWonder);
        }

    }

    public List<Player> playersInGame= new List<Player>();

    public void CreatePlayers()
    {
        int j=0;
        foreach (Toggle i in playersToggle)
        {
            
            if (i.isOn)
            {
                Color pColor = i.GetComponentInChildren<Text>().color;
                playersInGame.Add(new Player(pColor,defaultTech[0]));
                activePlayersNum++;
            }
            else
            {
                playersInGame.Add(null);
            }
            j++;
        }
    }

    public void CreatePanels()
    {
        float buttonWidth = 1080 / activePlayersNum;
        float xPosition = buttonWidth / 2;
        int j = 0,lastPanel=0;
        foreach (GameObject i in playersPanel)
        {
            if (playersInGame[j] != null)
            {
                i.GetComponentInChildren<Button>().transform.position = new Vector3(xPosition, 2100, 0);
                i.GetComponentInChildren<Button>().GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, 120);
                xPosition += buttonWidth;
                i.SetActive(true);
                GameObject dropdown;
                dropdown = bigPanel[j].transform.Find("Character/CharDropdown").gameObject;
                dropdownChar.Add(dropdown);
                dropdown = bigPanel[j].transform.Find("Technology/TechDropdown").gameObject;
                dropdownTech.Add(dropdown);
                bigPanel[j].SetActive(false);
                lastPanel = j;
            }
            else
            {
                dropdownChar.Add(null);
                dropdownTech.Add(null);
            }
            j++;
        }
        SetDiplomacy();
        bigPanel[lastPanel].SetActive(true);
    }

    public void SetKabul(int i)
    {
        playersInGame[i].kabul=kabulToggle[i].isOn;
    }

    public void SetCartagine(int i)
    {
        playersInGame[i].cartagine = cartagineToggle[i].isOn;
    }

    public void SetDiplomacy()
    {
        for (int j = 0; j < 4; j++)
        {
            if (playersInGame[j] != null)
            {
                int y = 600;
                int atkX = 1080 / activePlayersNum;
                for (int i = 0; i < 4; i++)
                {
                    if ((i != j) && (playersInGame[i] != null))
                    {
                        GameObject instanceObj;
                        instanceObj = Instantiate(diplomacyToggle, new Vector3(570, y, 0), new Quaternion(0, 0, 0, 0), bigPanel[j].transform);
                        instanceObj.GetComponentInChildren<Image>().color = playersInGame[i].pColor;
                        instanceObj.GetComponent<DiploToggle>().playerDiplo = i;
                        instanceObj.GetComponent<DiploToggle>().panelNumber = j;

                        instanceObj = Instantiate(attackPrefab, new Vector3(atkX, 150, 0), new Quaternion(0, 0, 0, 0), bigPanel[j].transform);
                        instanceObj.GetComponentInChildren<Image>().color = playersInGame[i].pColor;
                        instanceObj.GetComponent<AttackButton>().defender = i;
                        instanceObj.GetComponent<AttackButton>().attacker = j;
                        y -= 135;
                        atkX += 1080 / activePlayersNum;
                    }

                }
            }
        }
    }

    public void SetCharacter(int i)
    {
        playersInGame[i].pChar = dropdownChar[i].GetComponent<Dropdown>().value;
    }

    public void SetTechnology(int i)
    {
        int techLvl;
            techLvl = dropdownTech[i].GetComponent<Dropdown>().value;
            playersInGame[i].pTech = defaultTech[techLvl];
    }

    public void GenerateAtkPanel()
    {
        cartagDefValue = 0;
        cartagAtkValue = 0;
        cartagineAtkText.text = "+" + cartagAtkValue;
        cartagineDefText.text = "+" + cartagDefValue;
        cartagAtkDropdown.value = 0;
        cartagDefDropdown.value = 0;
        rinfTokenDropdown.value = 0;
        slotDropdown.value = 0;
        terrainDropdown.value = 0;
        atkCityToggle.isOn = false;
        atkChar.GetComponent<Image>().sprite = charImages[playersInGame[attacker].pChar];
        defChar.GetComponent<Image>().sprite = charImages[playersInGame[defender].pChar];

        atkWonderValue = 0;
        foreach(wonder i in playersInGame[attacker].pWonder)
        {
            atkWonderValue += i.atkBonus;
        }

        defWonderValue = 0;
        foreach (wonder i in playersInGame[defender].pWonder)
        {
            defWonderValue += i.defBonus;
        }
        atkWonderText.text = "+" + atkWonderValue;
        defWonderText.text = "+" + defWonderValue;

        allyAtkValue = 0;
        allyDefValue = 0;
        for(int i=0; i<4; i++)
        {
            if (i != defender)
                allyAtkValue += playersInGame[attacker].pDiploAtk[i];
            if (i != attacker)
                allyDefValue += playersInGame[defender].pDiploDef[i];
        }
        allyAtkText.text = "+" + allyAtkValue;
        allyDefText.text = "+" + allyDefValue;
       
        slotText.text = "+" + (slotDropdown.value + 1 + playersInGame[attacker].pTech.techBonus);
        UpdateTerrain();
    }

    public void EndOfAttack()
    {
        attackPanel.SetActive(false);
    }

}
