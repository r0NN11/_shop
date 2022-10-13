using System;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI.Wallet
{
    public class WalletContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountCoin;


        private void Start()
        {
            _textCountCoin.text = WalletController.Instance.GetCurrent_Coin().ToString("f0");
            WalletController.OnUpdateWallet += CalculateTextCount;
        }

        private void CalculateTextCount()
        {
            _textCountCoin.text = WalletController.Instance.GetCurrent_Coin().ToString("f0");
        }
    }
}