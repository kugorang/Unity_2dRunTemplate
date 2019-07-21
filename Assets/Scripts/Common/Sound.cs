using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class Sound
    {
        public string Name;
        
        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume;

        [Range(.1f, 3f)]
        public float Pitch;

        public bool Loop;

        public bool bPlaying = false;   // Mute에 사용할 용도

        [HideInInspector]
        public AudioSource Source;
    }
}