using Mechanics.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class HealthManager : BaseSingleton<HealthManager>
    {
        private const int MAX_HP = 3;

        [SerializeField] private List<GameObject> _healthUI;

        private int _currentHealth;
        private int _concumedHeal;

        private PlayerController _playerController;
        private GameManager _gameManager;

        protected override void Awake()
        {
            base.Awake();
            _playerController = FindObjectOfType<PlayerController>();
            _gameManager = GameManager.Instance;

            _playerController.OnDamage += OnPlayerDamage;
            _playerController.OnHeal += OnPlayerHeal;
        }

        private void Start()
        {
            _currentHealth = MAX_HP;
            UpdateHealth();
        }

        private void OnPlayerHeal()
        {
            if (_currentHealth >= MAX_HP) return;

            _concumedHeal++;

            if(_concumedHeal >= 3)
            {
                _currentHealth++;
                _concumedHeal = 0;
                UpdateHealth();
            }
        }

        private void OnPlayerDamage()
        {
            _currentHealth -= 1;
            UpdateHealth();

            if (_currentHealth <= 0) _gameManager.UpdateGameState(GameState.Ended);
        }

        private void UpdateHealth()
        {
            switch (_currentHealth)
            {
                case MAX_HP:
                    foreach (var item in _healthUI)
                    {
                        item.SetActive(true);
                    }
                    break;
                case 2:
                    for (int i = 0; i < _healthUI.Count; i++)
                    {
                        if (i == 2)
                        {
                            _healthUI[i].SetActive(false);
                        }
                        else
                        {
                            _healthUI[i].SetActive(true);
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < _healthUI.Count; i++)
                    {
                        if (i == 0)
                        {
                            _healthUI[i].SetActive(true);
                        }
                        else
                        {
                            _healthUI[i].SetActive(false);
                        }
                    }
                    break;
                case 0:
                    for (int i = 0; i < _healthUI.Count; i++)
                    {
                        _healthUI[i]?.SetActive(false);
                    }
                    break;
            }
        }


    }
}