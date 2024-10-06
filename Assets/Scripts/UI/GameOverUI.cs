using System;
using Snake.Input;
using UnityEngine;

namespace Snake.UI {
    public class GameOverUI : MonoBehaviour {
        [SerializeField] private Transform container;

        private void Start() {
            GameManager.Instance.OnGameOver += SnakeOnGameOver;
        }

        private void SnakeOnGameOver(object sender, EventArgs e) {
            Show();
        }


        private void Show() {
            container.gameObject.SetActive(true);
        }

        private void Hide() {
            container.gameObject.SetActive(false);
        }
    }
}