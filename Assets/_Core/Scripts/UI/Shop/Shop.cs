using System;
using System.Collections.Generic;
using _Core.Scripts.DataPersistence;
using _Core.Scripts.DataPersistence.Data;
using UnityEngine;
using System.Linq;

namespace _Core.Scripts.UI.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Transform _parentCustomCardBg;
        [SerializeField] private Transform _parentCustomCardSurface;
        [SerializeField] private GameObject _shop;

        private CustomCard _customCard;
        private List<CustomCard> _customCards;
        private List<CustomCard> _customCardsSurface;
        private SettingCustomCard _settingCustomCard;
        private List<string> _boughtCustomCards;
        private string _chosenCardBgId;
        private string _chosenCardSurfaceId;

        public void StartBgShop(List<SettingCustomCard> settingCustomCards, CustomCard customCard)
        {
            LoadData(DataPersistenceManager.instance.GetGameData());
            _customCard = customCard;
            _shop.SetActive(true);
            if (_customCards == null)
            {
                _customCards = new List<CustomCard>(settingCustomCards.Count);
                foreach (var cardSetting in settingCustomCards)
                {
                    var card = Instantiate(customCard, _parentCustomCardBg);
                    card.SetSettingCustomCard(cardSetting, Buy);
                    _customCards.Add(card);
                    if (!cardSetting.IsBuyCard()) continue;
                    card.SetBuyInactive();
                    if (cardSetting.GetId() == _chosenCardBgId)
                        card.SetImageBg(cardSetting);
                }
            }
            else
            {
                for (var i = 0; i < settingCustomCards.Count; i++)
                {
                    _customCards[i].gameObject.SetActive(true);
                    _customCards[i].SetSettingCustomCard(settingCustomCards[i], Buy);

                    if (!settingCustomCards[i].IsBuyCard()) continue;
                    _customCards[i].SetBuyInactive();
                    if (settingCustomCards[i].GetId() == _chosenCardBgId)
                        _customCards[i].SetImageBg(settingCustomCards[i]);
                }
            }

            _shop.SetActive(true);
        }

        public void StartSurfaceShop(List<SettingCustomCard> settingCustomCards, CustomCard customCard)
        {
            LoadData(DataPersistenceManager.instance.GetGameData());
            _customCard = customCard;
            if (_customCardsSurface == null)
            {
                _customCardsSurface = new List<CustomCard>(settingCustomCards.Count);
                foreach (var cardSetting in settingCustomCards)
                {
                    var card = Instantiate(customCard, _parentCustomCardSurface);
                    card.SetSettingCustomCard(cardSetting, BuySurface);
                    _customCardsSurface.Add(card);
                    if (!cardSetting.IsBuyCard()) continue;
                    card.SetBuyInactive();
                    if (cardSetting.GetId() == _chosenCardSurfaceId)
                        card.SetImageSurface(cardSetting);
                }
            }
            else
            {
                for (var i = 0; i < settingCustomCards.Count; i++)
                {
                    _customCardsSurface[i].gameObject.SetActive(true);
                    _customCardsSurface[i].SetSettingCustomCard(settingCustomCards[i], BuySurface);

                    if (!settingCustomCards[i].IsBuyCard()) continue;
                    _customCardsSurface[i].SetBuyInactive();
                    if (settingCustomCards[i].GetId() == _chosenCardSurfaceId)
                        _customCardsSurface[i].SetImageSurface(settingCustomCards[i]);
                }
            }

            _shop.SetActive(true);
        }

        private void CloseShop()
        {
            if (_customCards != null)
            {
                for (var i = 0; i < _customCards.Count; i++)
                {
                    _customCards[i].gameObject.SetActive(false);
                }
            }

            if (_customCardsSurface != null)
            {
                for (var i = 0; i < _customCardsSurface.Count; i++)
                {
                    _customCardsSurface[i].gameObject.SetActive(false);
                }
            }

            _shop.SetActive(false);
        }


        private void Buy(SettingCustomCard settingCustomCard)
        {
            var price = settingCustomCard.GetPrice();
            var id = settingCustomCard.GetId();
            var currentCoin = WalletController.Instance.GetCurrent_Coin();
            if (settingCustomCard.IsBuyCard())
                settingCustomCard.SetSelectObject();
            else if (price <= currentCoin)
            {
                currentCoin -= price;
                WalletController.Instance.Set_Coin(currentCoin);
                settingCustomCard.SetSelectObject();
                _boughtCustomCards.Add(id);
                DataPersistenceManager.instance.GetGameData().boughtCustomCards.Add(id);
            }

            DataPersistenceManager.instance.GetGameData().chosenCardBgId = settingCustomCard.GetId();
            CloseShop();
        }

        private void BuySurface(SettingCustomCard settingCustomCard)
        {
            var price = settingCustomCard.GetPrice();
            var id = settingCustomCard.GetId();
            var currentCoin = WalletController.Instance.GetCurrent_Coin();
            if (settingCustomCard.IsBuyCard())
                settingCustomCard.SetSelectObjectSurface();
            else if (price <= currentCoin)
            {
                currentCoin -= price;
                WalletController.Instance.Set_Coin(currentCoin);
                settingCustomCard.SetSelectObjectSurface();
                _boughtCustomCards.Add(id);
                DataPersistenceManager.instance.GetGameData().boughtCustomCards.Add(id);
            }

            DataPersistenceManager.instance.GetGameData().chosenCardSurfaceId = settingCustomCard.GetId();
            CloseShop();
        }

        private void LoadData(GameData data)
        {
            _boughtCustomCards = new List<string>();
            _boughtCustomCards.AddRange(data.boughtCustomCards);
            _chosenCardBgId = data.chosenCardBgId;
            _chosenCardSurfaceId = data.chosenCardSurfaceId;
        }

        public void SaveData(GameData data)
        {
            
        }
    }
}