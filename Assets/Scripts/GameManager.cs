using System;
using Snake.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;
        public EventHandler OnGameOver;


        private bool _gamePaused = false;

        private void Awake() {
            Instance = this;
        }

        private void OnEnable() {
            Time.timeScale = 1;
            GameInput.Instance.OnGameRestart += OnGameRestart;
            GameInput.Instance.OnMainMenuExit += OnMainMenuExit;
            GameInput.Instance.OnTogglePause += (sender, args) => TogglePause();
        }

        private void OnDisable() {
            GameInput.Instance.OnGameRestart -= OnGameRestart;
            GameInput.Instance.OnMainMenuExit -= OnMainMenuExit;
            GameInput.Instance.OnTogglePause -= (sender, args) => TogglePause();
        }


        private void OnMainMenuExit(object sender, EventArgs e) {
            SceneManager.LoadScene("MainMenuScene");
        }

        private void OnGameRestart(object sender, EventArgs e) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOver() {
            Time.timeScale = 0;
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }

        public void TogglePause() {
            _gamePaused = !_gamePaused;
            Time.timeScale = _gamePaused ? 0f : 1f;
        }
    }
}