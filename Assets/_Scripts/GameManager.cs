using System;
using UnityEngine;

namespace Common.Managers
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnGameStart;
        public event Action OnGameEnd;

        [field: SerializeField] public GameState GameState { get; private set; }

        private void Start()
        {
            //GameState = GameState.Playing;
        }

        public void UpdateGameState(GameState newState)
        {
            GameState = newState;

            switch (GameState)
            {
                case GameState.Paused:
                    PauseGame();
                    break;
                case GameState.Resumed:
                    ResumeGame();
                    break;
                case GameState.Playing:
                    PlayGame();
                    break;
                case GameState.Started:
                    StartGame();
                    break;
                case GameState.Ended:
                    EndGame();
                    break;
            }
        }

        private void PlayGame()
        {
            Time.timeScale = 1;
        }

        private void EndGame()
        {
            OnGameEnd?.Invoke();
        }

        private void StartGame()
        {
            Time.timeScale = 1.0f;
            OnGameStart?.Invoke();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            OnGameResume?.Invoke();
            UpdateGameState(GameState.Playing);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            OnGamePause?.Invoke();
        }
    }
}