using Common;
using UnityEngine;

namespace Play
{
    public class ButtonManager : MonoBehaviour
    {public void OnBtnClick(string soundName)
        {
            AudioManager.Instance.Play(soundName);
        }
    }
}
