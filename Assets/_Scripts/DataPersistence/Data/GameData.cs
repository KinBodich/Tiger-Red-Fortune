using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.DataPersistence
{
    [System.Serializable]
    public class GameData
    {
        public int CurrentBackground;
        public int MoneyCount;
        public bool[] BoughtBackgrounds = new bool[11];
        public bool[] BoughtMusic = new bool[7];
        public int CurrentMusic;

        public GameData()
        {
            CurrentBackground = 0;
            MoneyCount = 0;
            CurrentMusic = 0;

            BoughtBackgrounds = new bool[11];
            for (int i = 0; i < BoughtBackgrounds.Length; i++)
            {
                if (i == 0)
                {
                    BoughtBackgrounds[i] = true;
                    continue;
                }
                BoughtBackgrounds[i] = false;
            }

            BoughtMusic = new bool[7];
            for (int i = 0; i < BoughtMusic.Length; i++)
            {
                if (i == 0)
                {
                    BoughtMusic[i] = true;
                    continue;
                }
                BoughtMusic[i] = false;
            }
        }
    }
}