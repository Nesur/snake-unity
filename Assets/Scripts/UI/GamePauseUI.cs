using System;
using Snake.Input;
using UnityEngine;

namespace Snake.UI {
    public class GamePauseUI : MonoBehaviour {
        [SerializeField] private Transform container;


        private void OnEnable() {
            GameInput.Instance.OnTogglePause += OnTogglePause;
        }

        private void OnDisable() {
            GameInput.Instance.OnTogglePause -= OnTogglePause;
        }

        private void OnTogglePause(object sender, EventArgs e) {
            if (container.gameObject.activeInHierarchy) {
                Hide();
            }
            else {
                Show();
            }
        }

        private void Show() {
            container.gameObject.SetActive(true);
        }

        private void Hide() {
            container.gameObject.SetActive(false);
        }
    }
}