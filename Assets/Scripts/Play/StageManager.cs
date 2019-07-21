using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
	public class StageManager : MonoBehaviour
	{
		// 게임 종료 여부와 일시정지 여부를 판단하는 bool 변수
		[HideInInspector]
		public bool bGameOver, bPause;
	
		// 게임 시간과 관련된 변수
		private float oneFrameSecond;
		private WaitForSeconds waitForSeconds;
	
        // HP 관련 변수
        [SerializeField]
		private Image hpBar = null;

        [SerializeField]
		private float maxTimeSeconds = 0f;

		private int score;

        [SerializeField]
		private Text gameScoreText = null;
		
        [SerializeField]
		private GameObject scorePanel = null;

        // 아이템 풀
        [SerializeField]
        private GameObject itemPrefab = null;

        [SerializeField]
		private ItemObject commonItemSo = null, rareItemSo = null, boomItemSo = null;

		private List<GameObject> commonItemPool, epicItemPool, boomItemPool;

		private RankingManager rankingManager;
		private AudioManager audioManager;

		private void Start()
		{
			rankingManager = GetComponent<RankingManager>();
			audioManager = AudioManager.Instance;
			
			Init();
			
			if (!audioManager.IsPlay("Theme"))
            {
                audioManager.Play("Theme");
            }
				
			StartCoroutine("TimerOn");
			StartCoroutine("MakeItem");
		}

		public void Init()
		{
            ResumeGame();

            bGameOver = false;
			oneFrameSecond = 0.02f;
			waitForSeconds = new WaitForSeconds(oneFrameSecond);
			commonItemPool = new List<GameObject>();
			epicItemPool = new List<GameObject>();
			boomItemPool = new List<GameObject>();
			
			gameScoreText.text = 0.ToString();
			
			StopAllCoroutines();
		}

		private IEnumerator TimerOn()
		{
			var decreaseValue = 100 * oneFrameSecond / maxTimeSeconds / 60;
		
			while (hpBar.fillAmount > 0)
			{
				hpBar.fillAmount -= decreaseValue;
			
				yield return waitForSeconds;
			}

            bGameOver = true;
            PauseGame();

			audioManager.Stop("Theme");
			audioManager.Play("GameOver");

			rankingManager.ShowResult(score);

            scorePanel.SetActive(true);
        }

        public void PauseGame()
        {
            bPause = true;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            bPause = false;
            Time.timeScale = 1f;
        }

		private IEnumerator MakeItem()
		{
			while (!bGameOver)
			{
				yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));

				var randomValue = Random.Range(0, 100);
				
				if (randomValue > 60)
                {
                    GetBoomItem().SetActive(true);
                }
                else if (randomValue > 10)
                {
                    GetCommonItem().SetActive(true);
                }
				else
                {
                    GetEpicItem().SetActive(true);
                }	
			}
		}

		public void IncreaseScore(int increaseAmount)
		{
			score += increaseAmount;
			gameScoreText.text = score.ToString();
		
			rankingManager.CheckHighScore(score);
		}

		public void IncreaseHp(float gainHp)
		{
			hpBar.fillAmount += gainHp;
		}
		
		private GameObject GetCommonItem()
		{
			foreach (var item in commonItemPool)
			{
				if (item.activeSelf)
                {
                    continue;
                }
				
				return item;
			}

			var newItem = Instantiate(itemPrefab);
			var itemManager = newItem.GetComponent<ItemManager>();

			itemManager.ItemObject = commonItemSo;
			itemManager.Init();
				
			commonItemPool.Add(newItem);
			
			return newItem;
		}
		
		private GameObject GetEpicItem()
		{
			foreach (var item in epicItemPool)
			{
				if (item.activeSelf)
                {
                    continue;
                }	
				
				return item;
			}

			var newItem = Instantiate(itemPrefab);
			var itemManager = newItem.GetComponent<ItemManager>();

			itemManager.ItemObject = rareItemSo;
			itemManager.Init();
			
			epicItemPool.Add(newItem);
			
			return newItem;
		}
		
		private GameObject GetBoomItem()
		{
			foreach (var item in boomItemPool)
			{
				if (item.activeSelf)
                {
                    continue;
                }	
				
				return item;
			}

			var newItem = Instantiate(itemPrefab);
			var itemManager = newItem.GetComponent<ItemManager>();

			itemManager.ItemObject = boomItemSo;
			itemManager.Init();
			
			boomItemPool.Add(newItem);
			
			return newItem;
		}	
	}
}
