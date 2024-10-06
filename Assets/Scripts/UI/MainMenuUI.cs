using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Snake.UI {
    public class MainMenuUI : MonoBehaviour {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        // Start is called before the first frame update
        void Start() {
            playButton.onClick.AddListener(OnPlayPressed);
            exitButton.onClick.AddListener(OnExitPressed);
        }

        private void OnPlayPressed() {
            SceneManager.LoadScene("GameScene");
        }

        private void OnExitPressed() {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}