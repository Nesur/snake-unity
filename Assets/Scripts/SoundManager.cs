using System;
using Snake.Consumables;
using UnityEngine;

namespace Snake {
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour {
        public static SoundManager Instance;
        [SerializeField] private AudioClipListSO audioClipList;

        private AudioSource _audioSource;

        private void Awake() {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            ConsumableController.OnAnyConsume += ConsumableOnAnyConsume;
            Snake.OnChangeDirection += SnakeOnMovement;
        }

        private void OnDisable() {
            ConsumableController.OnAnyConsume -= ConsumableOnAnyConsume;
            Snake.OnChangeDirection -= SnakeOnMovement;
        }

        private void SnakeOnMovement(object sender, EventArgs e) {
            PlayClip(audioClipList.moveClip);
        }

        private void ConsumableOnAnyConsume(object sender, EventArgs e) {
            PlayClip(audioClipList.consumeClip);
        }

        private void PlayClip(AudioClip clip) {
            _audioSource.PlayOneShot(clip);
        }
    }
}