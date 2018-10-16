using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
	public class LevelChanger : Singleton<LevelChanger>
	{
		// guarantee this will be always a singleton only - can't use the constructor!
		// 아래 코드는 protected를 사용함으로써 생성자를 마음대로 사용할 수 없으므로 싱글톤을 보장합니다.
		protected LevelChanger() {}

		public Animator Animator;

		private enum ChangeMod
		{
			Int,
			String
		}

		private ChangeMod _changeMod;
		
		private int _levelToLoad;
		private string _sceneName;

		// If you want to create a singleton that is not destroyed when the scene is converted,
		//   use it like the following Awake function.
		// 만약에 씬이 변환되도 파괴되지 않은 싱글톤을 만들고 싶다면 아래 Awake 함수처럼 사용합니다.
		/*private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}*/

		public void FadeToLevel(int levelIndex)
		{
			_levelToLoad = levelIndex;
			_changeMod = ChangeMod.Int;
			
			Animator.SetTrigger("FadeOut");
		}
		
		public void FadeToLevel(string sceneName)
		{
			_sceneName = sceneName;
			_changeMod = ChangeMod.String;
			
			Animator.SetTrigger("FadeOut");
		}

		// The event function that executes when the animation finishes.
		// 애니메이션이 끝나면 실행하는 이벤트 함수입니다.
		public void OnFadeComplete()
		{
			switch (_changeMod)
			{
			case ChangeMod.Int:
				SceneManager.LoadScene(_levelToLoad);
				break;
			case ChangeMod.String:
				SceneManager.LoadScene(_sceneName);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}
