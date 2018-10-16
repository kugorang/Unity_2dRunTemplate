using UnityEngine;
using UnityEngine.UI;

namespace Play
{
	public class RankingManager : MonoBehaviour
	{
		/*// Static instance of GameManager which allows it to be accessed by any other script.
		public static RankingManager Instance;*/
		
		private int _highScore;
		public Text GameHighScoreText;
		
		public Text ResultScoreText;
		public Text ResultHighScoreText;
		
		/*private void Awake()
		{
			// Check if instance already exists
			if (Instance == null)
			{        
				// if not, set instance to this
				Instance = this;
			}
			//If instance already exists and it's not this:
			else if (Instance != this)
			{        
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);    
			}   

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}*/

		// Use this for initialization
		private void Start ()
		{
			_highScore = PlayerPrefs.GetInt("HighScore", 0);
			GameHighScoreText.text = _highScore.ToString();
		}

		public void CheckHighScore(int nowScore)
		{
			if (nowScore <= _highScore) 
				return;
		
			_highScore = nowScore;
			PlayerPrefs.SetInt("HighScore", nowScore);
			GameHighScoreText.text = _highScore.ToString();
		}

		public void ShowResult(int nowScore)
		{
			ResultScoreText.text = nowScore.ToString();
			ResultHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
		}
	}
}
