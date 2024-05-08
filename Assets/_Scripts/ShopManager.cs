using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Managers
{
    public class ShopManager : BaseSingleton<ShopManager>
    {
        private GameManager _gameManager;

        protected override void Awake()
        {
            base.Awake();

            _gameManager = GameManager.Instance;
            _gameManager.UpdateGameState(GameState.Menu);
        }

        public void CloseShop()
        {
            SceneManager.LoadScene(0);
        }
    }
}