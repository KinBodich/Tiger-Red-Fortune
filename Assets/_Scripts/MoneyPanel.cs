using Common.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Common.UI
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private MoneyManager _moneyManager;

        private void Awake()
        {
            _moneyManager = MoneyManager.Instance;

            _moneyManager.OnMoneyChange += OnMoneyChange;
        }

        private void Start()
        {
            _moneyText.SetText($"{_moneyManager.MoneyCount}");
        }

        private void OnMoneyChange()
        {
            _moneyText.SetText($"{_moneyManager.MoneyCount}");
        }
    }
}