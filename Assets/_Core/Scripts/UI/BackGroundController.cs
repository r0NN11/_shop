using System;
using _Core.Scripts.DataPersistence;
using _Core.Scripts.UI.Shop;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class BackGroundController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backGround;
        [SerializeField] private SpriteRenderer _surface;
        private float _distance;
        private ShopConfig _shopConfig;
        private ShopConfig _shopConfigSurface;

        private void Start()
        {
            _shopConfig = Resources.Load<ShopConfig>("ShopConfig");
            _shopConfigSurface = Resources.Load<ShopConfig>("ShopConfigSurface");
            SettingCustomCard.OnChangeSelectObject += SetBackGround;
            SettingCustomCard.OnChangeSelectObjectSurface += SetSurface;
            var data = DataPersistenceManager.instance.GetGameData();
            if (data.chosenCardBgId != null)
                if (data.chosenCardBgId.Length != 0)
                    _backGround.sprite = _shopConfig.GetSettingCustomCard()[Convert.ToInt32(data.chosenCardBgId) - 1]
                        .GetSpriteBg();
            if (data.chosenCardSurfaceId != null)
                if (data.chosenCardSurfaceId.Length != 0)
                    _surface.sprite =
                        _shopConfigSurface.GetSettingCustomCard()[Convert.ToInt32(data.chosenCardSurfaceId) - 21]
                            .GetSpriteSurface();
        }

        private void SetBackGround()
        {
            foreach (var settingCustomCard in _shopConfig.GetSettingCustomCard())
            {
                if (settingCustomCard.IsBuySelectObject())
                {
                    _backGround.sprite = settingCustomCard.GetSpriteBg();
                    break;
                }
            }
        }

        private void SetSurface()
        {
            foreach (var settingCustomCard in _shopConfigSurface.GetSettingCustomCard())
            {
                if (settingCustomCard.IsBuySelectObject())
                {
                    _surface.sprite = settingCustomCard.GetSpriteSurface();
                    break;
                }
            }
        }

        private void OnDestroy()
        {
            SettingCustomCard.OnChangeSelectObject -= SetBackGround;
        }
    }
}