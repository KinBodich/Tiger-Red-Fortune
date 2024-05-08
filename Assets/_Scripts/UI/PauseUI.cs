using Common.DataPersistence;
using Common.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;

        private GameManager _gameManager;
        private BackgroundMusicManager _backgroundMusicManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _backgroundMusicManager = BackgroundMusicManager.Instance;
        }

        public void PauseGame()
        {
            _gameManager.UpdateGameState(GameState.Paused);
            _pauseMenu.SetActive(true);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ResumeGame()
        {
            _pauseMenu?.SetActive(false);
            _gameManager.UpdateGameState(GameState.Resumed);
        }
    }
}