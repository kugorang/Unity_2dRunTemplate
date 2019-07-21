using System;
using UnityEngine;

namespace Common
{
	public class AudioManager : Singleton<AudioManager>
	{
		[SerializeField]
		private Sound[] sounds = null;

        private bool bCheckMute;

        public bool CheckMute
        {
            get
            {
                bCheckMute = PlayerPrefsExtension.GetBool("bCheckMute", false);

                return bCheckMute;
            }
            set
            {
                bCheckMute = value;
                PlayerPrefsExtension.SetBool("bCheckMute", value);
            }
        }
		
		// Use this for initialization
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			
			foreach (var s in sounds)
			{
				s.Source = gameObject.AddComponent<AudioSource>();
				s.Source.clip = s.Clip;
				s.Source.volume = s.Volume;
				s.Source.pitch = s.Pitch;
				s.Source.loop = s.Loop;
			}
		}

		private void Start()
		{
            if (!CheckMute)
            {
                Play("Theme");
            }
            else
            {
                FindSound("Theme").bPlaying = true;
            }
		}

		private Sound FindSound(string soundName)
		{
			var s = Array.Find(sounds, sound => sound.Name == soundName);

			if (s != null)
            {
                return s;
            }

            Debug.LogWarningFormat("Sound: {0} not found!", soundName);

			return null;
		}

		public void Play(string soundName)
		{
            if (bCheckMute)
            {
                return;
            }

			var s = FindSound(soundName);
			
			if (s == null)
            {
                return;
            }
			
			s.Source.Play();
		}
		
		public void Stop(string soundName)
		{
			var s = FindSound(soundName);
			
			if (s == null)
            {
                return;
            }
			
			s.Source.Stop();
		}

		public bool IsPlay(string soundName)
		{
			var s = FindSound(soundName);

			return s != null && s.Source.isPlaying;
		}

        public void Mute()
        {
            if (CheckMute)
            {
                return;
            }

            CheckMute = true;

            foreach (var s in sounds)
            {
                if (s.Source.isPlaying)
                {
                    s.bPlaying = true;
                }

                s.Source.Stop();
            }
        }

        public void Unmute()
        {
            if (!CheckMute)
            {
                return;
            }

            CheckMute = false;

            foreach (var s in sounds)
            {
                if (s.bPlaying)
                {
                    s.Source.Play();
                }
            }
        }
	}
}
