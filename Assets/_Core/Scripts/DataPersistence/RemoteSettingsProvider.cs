using System;
using _Core.Scripts.UI.Shop;
using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

namespace _Core.Scripts.DataPersistence

{
    public class RemoteSettingsProvider : MonoBehaviour
    {
        public ShopConfig _shopConfig;
        public ShopConfig _shopConfigSurface;
        public static RemoteSettingsProvider Instance { get; private set; }

        public struct userAttributes
        {
        }

        public struct appAttributes
        {
        }

        public ShopConfig GetShopConfig() => _shopConfig;
        public ShopConfig GetShopConfigSurface() => _shopConfigSurface;

        async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        private async void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _shopConfig = Resources.Load<ShopConfig>("ShopConfig");
            _shopConfigSurface = Resources.Load<ShopConfig>("ShopConfigSurface");
            await InitializeRemoteConfigAsync();
            RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(),
                new appAttributes());
            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        }

        private void ApplyRemoteSettings(Unity.Services.RemoteConfig.ConfigResponse response)
        {
            switch (response.requestOrigin)
            {
                case Unity.Services.RemoteConfig.ConfigOrigin.Default:
                    Debug.Log("No settings loaded this session; using default values.");
                    break;
                case Unity.Services.RemoteConfig.ConfigOrigin.Cached:
                    Debug.Log("No settings loaded this session; using cached values from a previous session.");
                    break;
                case Unity.Services.RemoteConfig.ConfigOrigin.Remote:
                    var shopConfigBg = RemoteConfigService.Instance.appConfig.GetJson("ShopConfigBg");
                    var shopConfigSurface = RemoteConfigService.Instance.appConfig.GetJson("ShopConfigSurface");
                    JsonUtility.FromJsonOverwrite(shopConfigBg, _shopConfig);
                    JsonUtility.FromJsonOverwrite(shopConfigSurface, _shopConfigSurface);
                    break;
            }
        }
    }
}