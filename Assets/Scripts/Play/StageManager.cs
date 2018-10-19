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
		public bool IsGameOver, IsPause;
	
		// 게임 시간과 관련된 변수
		private float _oneFrameSecond;
		private WaitForSeconds _waitForSeconds;
	
		public Image HpBar;
		public float MaxTimeSeconds;

		private int _score;
		public Text GameScoreText;
		
		public GameObject ScorePanel;
		
		// 아이템 풀
		public GameObject ItemPrefab;
		public ItemObject CommonItemSo, RareItemSo, BoomItemSo;
		private List<GameObject> _commonItemPool, _epicItemPool, _boomItemPool;

		private RankingManager _rankingManager;
		private AudioManager _audioManager;

		private void Start()
		{
			_rankingManager = GetComponent<RankingManager>();
			_audioManager = AudioManager.Instance;
			
			Init();
			
			if (!_audioManager.IsPlay("Theme"))
				_audioManager.Play("Theme");
			
			StartCoroutine("TimerOn");
			StartCoroutine("MakeItem");
		}

		private void Update()
		{
			if (!IsGameOver || IsPause) 
				return;

			IsPause = true;
			ScorePanel.SetActive(true);
		}

		public void Init()
		{
			IsGameOver = false;
			IsPause = false;
			_oneFrameSecond = 0.02f;
			_waitForSeconds = new WaitForSeconds(_oneFrameSecond);
			_commonItemPool = new List<GameObject>();
			_epicItemPool = new List<GameObject>();
			_boomItemPool = new List<GameObject>();
			
			GameScoreText.text = 0.ToString();
			
			StopAllCoroutines();
		}

		private IEnumerator TimerOn()
		{
			var decreaseValue = 100 * _oneFrameSecond / MaxTimeSeconds / 60;
		
			while (HpBar.fillAmount > 0)
			{
				HpBar.fillAmount -= decreaseValue;
			
				yield return _waitForSeconds;
			}

			IsGameOver = true;
			_audioManager.Stop("Theme");
			_audioManager.Play("GameOver");

			_rankingManager.ShowResult(_score);
		}

		private IEnumerator MakeItem()
		{
			while (!IsGameOver)
			{
				yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));

				var randomValue = Random.Range(0, 100);
				
				if (randomValue > 60)
					GetBoomItem().SetActive(true);
				else if (randomValue > 10)
					GetCommonItem().SetActive(true);
				else
					GetEpicItem().SetActive(true);
			}
		}

		public void IncreaseScore(int increaseAmount)
		{
			_score += increaseAmount;
			GameScoreText.text = _score.ToString();
		
			_rankingManager.CheckHighScore(_score);
		}

		public void IncreaseHp(float gainHp)
		{
			HpBar.fillAmount += gainHp;
		}
		
		private GameObject GetCommonItem()
		{
			foreach (var item in _commonItemPool)
			{
				if (item.activeSelf) 
					continue;
				
				return item;
			}

			var newItem = Instantiate(ItemPrefab);

			var itemManager = newItem.GetComponent<ItemManager>();
			itemManager.ItemObject = CommonItemSo;
			itemManager.Init();
				
			_commonItemPool.Add(newItem);
			
			return newItem;
		}
		
		private GameObject GetEpicItem()
		{
			foreach (var item in _epicItemPool)
			{
				if (item.activeSelf)
					continue;
				
				return item;
			}

			var newItem = Instantiate(ItemPrefab);
			
			var itemManager = newItem.GetComponent<ItemManager>();
			itemManager.ItemObject = RareItemSo;
			itemManager.Init();
			
			_epicItemPool.Add(newItem);
			
			return newItem;
		}
		
		private GameObject GetBoomItem()
		{
			foreach (var item in _boomItemPool)
			{
				if (item.activeSelf)
					continue;
				
				return item;
			}

			var newItem = Instantiate(ItemPrefab);
			
			var itemManager = newItem.GetComponent<ItemManager>();
			itemManager.ItemObject = BoomItemSo;
			itemManager.Init();
			
			_boomItemPool.Add(newItem);
			
			return newItem;
		}	
	}
}
