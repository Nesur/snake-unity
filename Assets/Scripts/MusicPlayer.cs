using System;
using UnityEngine;

namespace Snake {
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour {
        public static MusicPlayer Instance;
        private AudioSource _audioSource;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
}