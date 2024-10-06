using TMPro;
using UnityEngine;

namespace Snake.UI {
    public class GameInfoUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI scoreText;

        // Start is called before the first frame update
        void OnEnable() {
            Snake.OnGrow += SnakeOnGrow;
        }

        private void SnakeOnGrow(object sender, Snake.OnGrowEventHandlerArgs e) {
            scoreText.text = $"Score: {e.Size}";
        }
    }
}