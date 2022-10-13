using System;
using _Core.Scripts.DataPersistence;
using _Core.Scripts.DataPersistence.Data;
using UnityEngine;

namespace _Core.Scripts
{
    public class WalletController : MonoBehaviour
    {
        private static WalletController _instance;

        public static WalletController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WalletController>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("WalletController");
                        _instance = container.AddComponent<WalletController>();
                    }
                }

                return _instance;
            }
        }

        public static event Action OnUpdateWallet;

        private int _coinAmount = 0;

        public int GetCurrent_Coin() => _coinAmount;

        public void Set_Coin(int coin)
        {
            _coinAmount = coin;
            OnUpdateWallet?.Invoke();
        }

        private void Awake()
        {
            LoadData(DataPersistenceManager.instance.GetGameData());
        }

        private void OnApplicationQuit()
        {
            SaveData(DataPersistenceManager.instance.GetGameData());
        }

        private void LoadData(GameData data)
        {
            _coinAmount = data.coinAmount;
        }

        private void SaveData(GameData data)
        {
            data.coinAmount = _coinAmount;
        }
    }
}