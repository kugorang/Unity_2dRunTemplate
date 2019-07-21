using UnityEngine;
using UnityEngine.UI;

namespace Play
{
	public class RankingManager : MonoBehaviour
	{		
		private int highScore;

        public int HighScore
        {
            get
            {
                highScore = PlayerPrefs.GetInt("HighScore", 0);

                return highScore;
            }
            set
            {
                PlayerPrefs.SetInt("HighScore", value);

                highScore = value;
            }
        }

        private bool bCheckHighScore;

        [SerializeField]
		private Text gameHighScoreText = null, resultScoreText = null, resultHighScoreText = null;

        [SerializeField]
        private Image medalImage = null;

		// Use this for initialization
		private void Start ()
		{
			gameHighScoreText.text = HighScore.ToString();
		}

		public void CheckHighScore(int nowScore)
		{
			if (nowScore <= highScore) 
				return;

            bCheckHighScore = true;
            HighScore = nowScore;
			gameHighScoreText.text = highScore.ToString();
		}

		public void ShowResult(int nowScore)
		{
			resultScoreText.text = nowScore.ToString();
			resultHighScoreText.text = highScore.ToString();

            if (bCheckHighScore)
            {
                medalImage.gameObject.SetActive(true);
            }
		}
	}
}
