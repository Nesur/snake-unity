using System.Collections.Generic;
using UnityEngine;

namespace Snake {
    public abstract class Utils {
        private Utils() {
        }

        public static T GetRandomListElement<T>(IList<T> list) {
            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}