using System;
using _Core.Scripts.DataPersistence;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI.Wallet
{
    public class WalletContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountCoin;


        private void Start()
        {
            _textCountCoin.text = DataPersistenceManager.instance.GetGameData().coinAmount.ToString("f0");
            DataPersistenceManager.OnUpdateGameData += CalculateTextCount;
        }

        private void CalculateTextCount()
        {
            _textCountCoin.text = DataPersistenceManager.instance.GetGameData().coinAmount.ToString("f0");
        }
    }
}