using UnityEngine;

namespace Snake.Consumables {
    [CreateAssetMenu(fileName = "Small apple factory", menuName = "Consumable Factory/Small apple")]
    public class SmallAppleFactory : ConsumableFactory {
        public override IConsumable CreateConsumable() {
            return new SmallApple();
        }
    }
}