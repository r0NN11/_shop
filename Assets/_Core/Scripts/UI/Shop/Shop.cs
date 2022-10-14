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
        [SerializeField] private GameObject _shopBg;
        [SerializeField] private GameObject _shopSurface;
        private List<CustomCard> _customCards;
        private List<CustomCard> _customCardsSurface;
        private List<string> _boughtCustomCards;
        private string _chosenCardBgId;
        private string _chosenCardSurfaceId;

        public void StartBgShop(List<SettingCustomCard> settingCustomCards, CustomCard customCard)
        {
            if (_customCards == null)
                _customCards = new List<CustomCard>(settingCustomCards.Count);
            LoadData(DataPersistenceManager.instance.GetGameData());
            StartShop(_customCards, settingCustomCards, customCard, _parentCustomCardBg, Buy, _shopBg);
        }

        public void StartSurfaceShop(List<SettingCustomCard> settingCustomCards, CustomCard customCard)
        {
            if (_customCardsSurface == null)
                _customCardsSurface = new List<CustomCard>(settingCustomCards.Count);
            LoadData(DataPersistenceManager.instance.GetGameData());
            StartShop(_customCardsSurface, settingCustomCards, customCard, _parentCustomCardSurface, BuySurface,
                _shopSurface);
        }

        private void StartShop(List<CustomCard> customCards, List<SettingCustomCard> settingCustomCards,
            CustomCard customCard, Transform shopParent, Action<SettingCustomCard> actionBuy, GameObject shop)
        {
            if (customCards.Count == 0)
            {
                foreach (var cardSetting in settingCustomCards)
                {
                    var card = Instantiate(customCard, shopParent);
                    card.SetSettingCustomCard(cardSetting, actionBuy);
                    customCards.Add(card);
                    if (!cardSetting.IsBuyCard())
                        continue;
                    card.SetBuyInactive();
                    CheckChosenCard(cardSetting, card);
                }
            }
            else
            {
                for (var i = 0; i < settingCustomCards.Count; i++)
                {
                    shop.SetActive(true);
                    customCards[i].SetSettingCustomCard(settingCustomCards[i], actionBuy);

                    if (!settingCustomCards[i].IsBuyCard())
                        continue;
                    customCards[i].SetBuyInactive();
                    CheckChosenCard(settingCustomCards[i], customCards[i]);
                }
            }

            _shop.SetActive(true);
        }

        private void CheckChosenCard(SettingCustomCard settingCustomCard, CustomCard card)
        {
            if (settingCustomCard.GetId() == _chosenCardBgId)
                card.SetImageBg(settingCustomCard);
            else if (settingCustomCard.GetId() == _chosenCardSurfaceId)
                card.SetImageSurface(settingCustomCard);
        }

        private void CloseShop()
        {
            if (_customCards != null)
                _shopBg.SetActive(false);

            if (_customCardsSurface != null)
                _shopSurface.SetActive(false);
            _shop.SetActive(false);
        }


        private void Buy(SettingCustomCard settingCustomCard)
        {
            var price = settingCustomCard.GetPrice();
            var id = settingCustomCard.GetId();
            var currentCoin = DataPersistenceManager.instance.GetGameData().coinAmount;
            if (settingCustomCard.IsBuyCard())
                settingCustomCard.SetSelectObject();
            else if (price <= currentCoin)
            {
                currentCoin -= price;
                DataPersistenceManager.instance.GetGameData().SetCoinAmount(currentCoin);
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
            var currentCoin = DataPersistenceManager.instance.GetGameData().coinAmount;
            if (settingCustomCard.IsBuyCard())
                settingCustomCard.SetSelectObjectSurface();
            else if (price <= currentCoin)
            {
                currentCoin -= price;
                DataPersistenceManager.instance.GetGameData().SetCoinAmount(currentCoin);
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