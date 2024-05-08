using Common.Managers;
using Common.UI;
using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.Wheel
{
    public class WheelSpin : MonoBehaviour
    {
        [SerializeField] private Button _uiSpinButton;
        [SerializeField] private TextMeshProUGUI _uiSpinButtonText;

        [SerializeField] private PickerWheel _pickerWheel;

        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _wheelPanel;
        [SerializeField] private ParticleSystem _confetti;
        
        private NoMoneyUI _noMoneyUI;
        private MoneyManager _moneyManager;

        private void Awake()
        {
            _moneyManager = MoneyManager.Instance;
            _noMoneyUI = FindObjectOfType<NoMoneyUI>();
        }

        private void Start()
        {
            _uiSpinButton.onClick.AddListener(() =>
            {
                if(_moneyManager.MoneyCount < 2000)
                {
                    _noMoneyUI.ShowPanel();
                    return;
                }

                _wheelPanel.SetActive(true);
                _closeButton.gameObject.SetActive(false);
                _moneyManager.ChangeMoney(-2000);

                _uiSpinButton.interactable = false;

                _pickerWheel.OnSpinEnd(wheelPiece =>
                {
                    Debug.Log(
                       @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                       + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                    );

                    _moneyManager.ChangeMoney(wheelPiece.Amount);

                    _uiSpinButton.interactable = true;
                    _confetti.Play();
                    _closeButton.gameObject.SetActive(true);
                });

                _pickerWheel.Spin();

            });

            _closeButton.onClick.AddListener(() =>
            {
                _wheelPanel.SetActive(false);
            });
        }
    }
}