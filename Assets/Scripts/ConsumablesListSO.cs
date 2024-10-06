using System.Collections.Generic;
using Snake.Consumables;
using UnityEngine;

namespace Snake {
    [CreateAssetMenu(fileName = "Consumables", menuName = "Consumables")]
    public class ConsumablesListSO : ScriptableObject {
        public List<Transform> consumablesPrefabsList;

        public Transform GetRandomConsumable() {
            return Utils.GetRandomListElement(consumablesPrefabsList);
        }
    }
}