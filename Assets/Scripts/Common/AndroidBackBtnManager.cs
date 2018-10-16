using UnityEngine;

namespace Common
{
	public class AndroidBackBtnManager : Singleton<AndroidBackBtnManager>
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void FixedUpdate ()
		{
			if (Application.platform != RuntimePlatform.Android) 
				return;

			if (!Input.GetKey(KeyCode.Escape)) 
				return;
		
			Application.Quit();
 
			System.Diagnostics.Process.GetCurrentProcess().Kill();
 
			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call<bool>("moveTaskToBack", true);
		}
	}
}