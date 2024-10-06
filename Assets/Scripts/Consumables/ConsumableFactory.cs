using UnityEngine;

namespace Snake.Consumables {
    public abstract class ConsumableFactory : ScriptableObject {

        public abstract IConsumable CreateConsumable();
    }
}