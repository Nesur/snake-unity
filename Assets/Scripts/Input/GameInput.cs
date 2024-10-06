using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake.Input {
    public class GameInput : MonoBehaviour {
        public static GameInput Instance;
        public EventHandler OnMoveUp;
        public EventHandler OnMoveDown;
        public EventHandler OnMoveLeft;
        public EventHandler OnMoveRight;
        public EventHandler OnGameRestart;
        public EventHandler OnMainMenuExit;
        public EventHandler OnTogglePause;

        private PlayerInputActions _playerInputActions;

        private void Awake() {
            Instance = this;
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        private void Start() {
            _playerInputActions.Player.MoveUp.performed += OnMoveUpPerformed;
            _playerInputActions.Player.MoveDown.performed += OnMoveDownPerformed;
            _playerInputActions.Player.MoveLeft.performed += OnMoveLeftPerformed;
            _playerInputActions.Player.MoveRight.performed += OnMoveRightPerformed;
            _playerInputActions.Player.Pause.performed += OnPaused;
            _playerInputActions.Player.Restart.performed += OnRestartPerformed;
            _playerInputActions.Player.Exit.performed += OnExitPerformed;
        }

        private void OnPaused(InputAction.CallbackContext obj) {
            OnTogglePause?.Invoke(this, EventArgs.Empty);
        }
        private void OnExitPerformed(InputAction.CallbackContext obj) {
            OnMainMenuExit?.Invoke(this, EventArgs.Empty);
        }
        private void OnRestartPerformed(InputAction.CallbackContext obj) {
            OnGameRestart?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveUpPerformed(InputAction.CallbackContext obj) {
            OnMoveUp?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveDownPerformed(InputAction.CallbackContext obj) {
            OnMoveDown?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveRightPerformed(InputAction.CallbackContext obj) {
            OnMoveRight?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveLeftPerformed(InputAction.CallbackContext obj) {
            OnMoveLeft?.Invoke(this, EventArgs.Empty);
        }
    }
}