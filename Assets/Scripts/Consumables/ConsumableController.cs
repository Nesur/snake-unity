using System;
using UnityEngine;

namespace Snake.Consumables {
    public class ConsumableController : MonoBehaviour {
        public static EventHandler OnAnyConsume;
        [SerializeField] private ConsumableFactory consumableFactory;

        private IConsumable _consumable = IConsumable.CreateDefault();

        private void Awake() {
            if (consumableFactory != null) {
                _consumable = consumableFactory.CreateConsumable();
            }
        }

        public void Consume(Snake snake) {
            _consumable.Consume(snake);
            ConsumableSpawnManager.Instance.Spawn();
            OnAnyConsume?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}