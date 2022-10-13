using System;
using System.Collections.Generic;
using _Core.Scripts.DataPersistence;
using UnityEngine;

namespace _Core.Scripts.UI.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private CustomCard _customCardBG;
        [SerializeField] private CustomCard _customCardSurface;
        private ShopConfig _shopConfig;
        private ShopConfig _shopConfigSurface;

        public void OpenBgShop()
        {
            _shop.StartBgShop(_shopConfig.GetSettingCustomCard(), _customCardBG);
        }

        public void OpenSurfaceShop()
        {
            _shop.StartSurfaceShop(_shopConfigSurface.GetSettingCustomCard(),
                _customCardSurface);
        }


        private void Start()
        {
            _shopConfig = Resources.Load<ShopConfig>("ShopConfig");
            _shopConfigSurface = Resources.Load<ShopConfig>("ShopConfigSurface");
            _shopConfig.SubscribeSelectCard();
            _shopConfigSurface.SubscribeSelectCard();
        }

        private void OnDestroy()
        {
            _shopConfig.UnSubscribeSelectCard();
        }
    }
}