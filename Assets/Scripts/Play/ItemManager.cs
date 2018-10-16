using UnityEngine;

namespace Play
{
	public class ItemManager : MonoBehaviour
	{
		public float ScrollSpeed;
		[HideInInspector] public ItemObject ItemObject;
		private StageManager _stageManager;
		
		private void Start()
		{
			_stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
		}
	
		// Update is called once per frame
		private void Update()
		{
			transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed);
		}

		public void Init()
		{
			GetComponent<SpriteRenderer>().sprite = ItemObject.Image;
			transform.position = new Vector2(3.223f, Random.Range(-0.3f, 0.55f));
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player")) 
				return;

			_stageManager.IncreaseScore(ItemObject.Score);
			_stageManager.IncreaseHp(ItemObject.GainHp);
			_stageManager.PlaySoundEffect(ItemObject.Sound);
			
			gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			transform.position = new Vector2(3.223f, Random.Range(-0.3f, 0.55f));
		}
	}
}