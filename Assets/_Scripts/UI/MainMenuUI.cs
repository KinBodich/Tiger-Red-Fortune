using Common.DataPersistence;
using Common.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        public void PlayGame()
        {
            _gameManager.UpdateGameState(GameState.Playing);
            SceneManager.LoadScene(1);
        }

        public void LoadShop()
        {
            SceneManager.LoadScene(2);
        }
    }
}