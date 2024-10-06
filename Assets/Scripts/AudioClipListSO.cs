using UnityEngine;

namespace Snake {
    [CreateAssetMenu(fileName = "AudioClipList", menuName = "AudioClipList")]
    public class AudioClipListSO : ScriptableObject {
        public AudioClip consumeClip;
        public AudioClip moveClip;
    }
}