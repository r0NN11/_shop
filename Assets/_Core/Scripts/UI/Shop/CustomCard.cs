using System;
using _Core.Scripts.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace _Core.Scripts.UI.Shop
{
    public class CustomCard : MonoBehaviour
    {
        [SerializeField] private Image _imageIconPrice;
        [SerializeField] private TextMeshProUGUI _textPriceCard;
        [SerializeField] private GameObject _buttonBuy;
        [SerializeField] private Outline _outline;
        [SerializeField] private GameObject block;
        [SerializeField] private Button _buttonSelect;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _backGroundImage;
        private SettingCustomCard _settingCustomCard;
        private CustomCard _customCard;
        private Action<SettingCustomCard> _actionBuy;

        public void SetSettingCustomCard(SettingCustomCard settingCustomCard,
            Action<SettingCustomCard> actionBuy)
        {
            _actionBuy = actionBuy;
            _settingCustomCard = settingCustomCard;
            var price = _settingCustomCard.GetPrice();
            var currentCoin = DataPersistenceManager.instance.GetGameData().coinAmount;
            _imageIconPrice.sprite = _settingCustomCard.GetSpriteIcon();
            _textPriceCard.text = price.ToString("f0");
            if (settingCustomCard.IsBuyCard())
            {
                _buttonSelect.enabled = true;
            }

            else if (currentCoin < price)
            {
                block.SetActive(true);
            }
        }

        public void BuyClick()
        {
            _actionBuy?.Invoke(_settingCustomCard);
        }

        public void SetBuyInactive()
        {
            _buttonBuy.SetActive(false);
        }

        public void SetImageBg(SettingCustomCard settingCustomCard)
        {
            _backGroundImage.enabled = false;
            _outline.enabled = false;
            _iconImage.sprite = settingCustomCard.GetSpriteBg();
        }

        public void SetImageSurface(SettingCustomCard settingCustomCard)
        {
            _backGroundImage.enabled = false;
            _outline.enabled = false;
            _iconImage.sprite = settingCustomCard.GetSpriteSurface();
        }
    }
}