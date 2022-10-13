using System.Collections.Generic;
using System.IO;
using _Core.Scripts.UI.Shop;
using UnityEngine;

namespace _Core.Scripts.DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        public int coinAmount;
        public List<string> boughtCustomCards;
        public string chosenCardBgId;
        public string chosenCardSurfaceId;

        public GameData()
        {
            coinAmount = 500;
            boughtCustomCards = new List<string>();
        }
    }
}