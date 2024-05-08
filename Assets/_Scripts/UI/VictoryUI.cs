using Common.Managers;
using Mechanics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.UI
{
    public class VictoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _victoryMenu;
        [SerializeField] private TextMeshProUGUI _moneyResultText;
        [SerializeField] private Button _pauseButton;

        private AudioSource[] _audioSources;
        private ResourceCollector _resourceCollector;
        private GameManager _gameManager;
        private MoneyManager _moneyManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _moneyManager = MoneyManager.Instance;
            _resourceCollector = FindObjectOfType<ResourceCollector>();
            _audioSources = GetComponents<AudioSource>();

            _gameManager.OnGameEnd += OnGameEnd;
        }

        private void OnGameEnd()
        {
            _victoryMenu.SetActive(true);
            _moneyResultText.SetText(_resourceCollector.MoneyCount.ToString());
            _moneyManager.ChangeMoney(_resourceCollector.MoneyCount);
            _pauseButton.interactable = false;
            foreach(var clip in _audioSources)
            {
                clip.Play();
            }
        }

        public void Close()
        {
            SceneManager.LoadScene(0);
        }
    }
}