using UnityEngine;
using Random = UnityEngine.Random;

namespace Snake {
    public class ConsumableSpawnManager : MonoBehaviour {
        public static ConsumableSpawnManager Instance;

        [SerializeField] private Snake snake;
        [SerializeField] private ConsumablesListSO consumablesListSo;
        [SerializeField] private Vector2 minSpawnPosition;
        [SerializeField] private Vector2 maxSpawnPosition;

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            SpawnRandomConsumable();
        }

        private void SpawnRandomConsumable() {
            Vector3 randomPosition;
            do {
                randomPosition = GenerateRandomSpawnPosition();
            } while (snake.GetFullSnakePositions().IndexOf(randomPosition) != -1);

            var randomConsumable = consumablesListSo.GetRandomConsumable();
            Instantiate(randomConsumable, randomPosition, randomConsumable.rotation);
        }


        private Vector3 GenerateRandomSpawnPosition() {
            var xPosition = Mathf.Round(Random.Range(minSpawnPosition.x, maxSpawnPosition.x));
            var zPosition = Mathf.Round(Random.Range(minSpawnPosition.y, maxSpawnPosition.y));
            return new Vector3(xPosition, 0f, zPosition);
        }

        public void Spawn() {
            SpawnRandomConsumable();
        }
    }
}