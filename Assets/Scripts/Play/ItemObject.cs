using System;
using UnityEngine;

namespace Play
{
	[CreateAssetMenu(fileName = "ItemObjectSO", menuName = "Create ItemObject")]
	public class ItemObject : ScriptableObject
	{
		public int Score;
		public float GainHp;
		public Sprite Image;
		public AudioClip Sound;
	}
}