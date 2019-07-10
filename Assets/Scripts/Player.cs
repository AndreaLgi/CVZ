using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        public Color pColor;
        public int pChar = 0;
        public Manager.technology pTech;
        public List<Manager.wonder> pWonder = new List<Manager.wonder>();
        public List<int> pDiploAtk = new List<int>();
        public List<int> pDiploDef = new List<int>();
        public bool cartagine = false;
        public bool kabul = false;


        public Player(Color ownColor, Manager.technology ownTech)
        {
            pColor = ownColor;
            pTech = ownTech;
            for (int i = 0; i < 4; i++)
            {
                pDiploAtk.Add(0);
                pDiploDef.Add(0);
            }
        }


        public void AddWonder(Manager.wonder ownWonder)
        {
            bool alreadyOwned = false;
            foreach (Manager.wonder i in pWonder)
                if (i.name == ownWonder.name)
                    alreadyOwned = true;
            if (!alreadyOwned)
                pWonder.Add(ownWonder);
        }

        public void RemoveWonder(Manager.wonder remWonder)
        {
            pWonder.Remove(remWonder);
        }


}
