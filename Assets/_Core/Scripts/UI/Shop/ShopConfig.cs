using System;
using System.Collections.Generic;
using _Core.Scripts.DataPersistence;
using UnityEngine;
using Unity.RemoteConfig;

namespace _Core.Scripts.UI.Shop
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "ShopConfig")]
    public class ShopConfig : ScriptableObject
    {
        [SerializeField] private List<SettingCustomCard> _settingCustomCards;
        public List<SettingCustomCard> GetSettingCustomCard() => _settingCustomCards;

        public void SubscribeSelectCard()
        {
            for (var i = 0; i < _settingCustomCards.Count; i++)
            {
                _settingCustomCards[i].SubscribeChangeLevelDisableObject();
            }
        }

        public void UnSubscribeSelectCard()
        {
            for (var i = 0; i < _settingCustomCards.Count; i++)
            {
                _settingCustomCards[i].UnSubscribeChangeLevelDisableObject();
            }
        }
    }

    [Serializable]
    public class SettingCustomCard
    {
        [SerializeField] private string _nameSaveSelectObject;
        [SerializeField] private int _price;
        [SerializeField] private string _id;
        [SerializeField] private string _spriteIconName;
        [SerializeField] private string _spriteBgName;
        [SerializeField] private string _spriteSurfaceName;
        private static Action OnChangeSelectObjectDisable;
        public static Action OnChangeSelectObject;
        public static Action OnChangeSelectObjectSurface;
        public int GetPrice() => _price;
        public Sprite GetSpriteIcon() => Resources.Load<Sprite>("Sprites/" + _spriteIconName);
        public Sprite GetSpriteBg() => Resources.Load<Sprite>("Sprites/Bg/" + _spriteBgName);
        public Sprite GetSpriteSurface() => Resources.Load<Sprite>("Sprites/Surface/" + _spriteSurfaceName);
        public string GetId() => _id;
        public void SubscribeChangeLevelDisableObject() => OnChangeSelectObjectDisable += SetSelectObjectDisable;
        public void UnSubscribeChangeLevelDisableObject() => OnChangeSelectObjectDisable -= SetSelectObjectDisable;
        public bool IsBuySelectObject() => PlayerPrefs.GetInt(_nameSaveSelectObject) > 0;
        public bool IsBuyCard() => DataPersistenceManager.instance.GetGameData().boughtCustomCards.Contains(_id);

        public void SetSelectObject()
        {
            OnChangeSelectObjectDisable?.Invoke();
            PlayerPrefs.SetInt(_nameSaveSelectObject, 1);
            PlayerPrefs.Save();
            OnChangeSelectObject?.Invoke();
        }

        public void SetSelectObjectSurface()
        {
            OnChangeSelectObjectDisable?.Invoke();
            PlayerPrefs.SetInt(_nameSaveSelectObject, 1);
            PlayerPrefs.Save();
            OnChangeSelectObjectSurface?.Invoke();
        }

        public void SetSelectObjectDisable()
        {
            PlayerPrefs.SetInt(_nameSaveSelectObject, 0);
            PlayerPrefs.Save();
        }
    }
}