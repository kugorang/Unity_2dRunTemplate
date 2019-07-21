using Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Play
{
    public class ButtonManager : MonoBehaviour
    {
        enum SOUND
        {
            OFF = 0,
            ON = 1
        }

        private AudioManager audioManager;

        [SerializeField]
        private StageManager stageManager;

        [SerializeField]
        private Image soundBtn = null;

        [SerializeField]
        private Sprite[] soundOnOffSprite = null;  // 0: off, 1: on

        private void Start()
        {
            audioManager = AudioManager.Instance;

            if (audioManager.CheckMute)
            {
                soundBtn.sprite = soundOnOffSprite[(int)SOUND.OFF];
            }
        }

        public void OnBtnClick(string soundName)
        {
            audioManager.Play(soundName);
        }

        public void OnSoundOnOffClick()
        {
            if (audioManager.CheckMute)
            {
                audioManager.Unmute();
                soundBtn.sprite = soundOnOffSprite[(int)SOUND.ON];
            }
            else
            {
                audioManager.Mute();
                soundBtn.sprite = soundOnOffSprite[(int)SOUND.OFF];
            }

        }

        public void OnResetClick()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
