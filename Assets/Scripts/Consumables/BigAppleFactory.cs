using UnityEngine;

namespace Snake.Consumables {
    [CreateAssetMenu(fileName = "Big apple factory", menuName = "Consumable Factory/Big apple")]
    public class BigAppleFactory : ConsumableFactory {
        public override IConsumable CreateConsumable() {
            return new BigApple();
        }
    }
}