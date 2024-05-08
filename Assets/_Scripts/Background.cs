using Common.DataPersistence;
using Common.Managers;
using Common.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Common
{
    public class Background : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private int _index;
        [SerializeField] private int _price;
        [SerializeField] private bool _isBought;

        [SerializeField] private GameObject _selectButton;
        [SerializeField] private GameObject _buyPanel;
        [SerializeField] private TextMeshProUGUI _priceText;
        [Space(5)]
        [SerializeField] private AudioSource _buyMusic;

        private MoneyManager _moneyManager;
        private BackgroundManager _backgroundManager;
        private NoMoneyUI _noMoneyUI;

        private void Awake()
        {
            _moneyManager = MoneyManager.Instance;
            _backgroundManager = BackgroundManager.Instance;
            _noMoneyUI = FindObjectOfType<NoMoneyUI>();
        }

        private void Start()
        {
            CheckBackground();
            _priceText.SetText($"{_price}");
        }

        public void BuyBackground()
        {
            if (_moneyManager.MoneyCount < _price)
            {
                _noMoneyUI.ShowPanel();
                return;
            }
            _moneyManager.ChangeMoney(-_price);
            _isBought = true;
            CheckBackground();
            SelectBackground();
            _buyMusic.Play();
        }

        public void SelectBackground()
        {
            _backgroundManager.SetBackground(_index);
        }

        private void CheckBackground()
        {
            if (_isBought)
            {
                _selectButton.SetActive(true);
                _buyPanel.SetActive(false);
            }
            else
            {
                _selectButton.SetActive(false);
                _buyPanel.SetActive(true);
            }
        }

        public void LoadData(GameData data)
        {
            _isBought = data.BoughtBackgrounds[_index];
        }

        public void SaveData(GameData data)
        {
            data.BoughtBackgrounds[_index] = _isBought;
        }
    }
}