using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieGame.Player;

namespace ZombieGame
{
	public class GameManager : MonoBehaviour
	{
		
		public static GameManager Instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				return null;
			}
		}
		private static GameManager _instance;

		private void Awake()
		{
			if (_instance != null)
				Destroy(this);
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
		public int MaximumHealth;
		public int MaximumArmour;
		public int StartingPoints;
		public int CurrentPoints;

		private void Start()
		{
			Player.PManager.Instance.Setup();
		}

		public void AddPoints(int amount)
		{
			CurrentPoints = CurrentPoints + amount;
		}

		public void RemovePoints(int amount)
		{
			CurrentPoints = CurrentPoints - amount;
		}

		public bool HasEnoughPoiints(int amount)
		{
			if (CurrentPoints - amount >= 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}