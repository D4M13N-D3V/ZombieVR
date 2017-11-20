using System;
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

		private void Start()
		{
			Setup();
		}
		
		
		
		/// <summary>
		/// The maximum amount of health the player has, and the amount they start with
		/// </summary>
		[Tooltip("The maximum amount of health the player has, and the amount they start with")]
		public int MaximumHealth;
		
		/// <summary>
		/// The amount of points the player starts with.
		/// </summary>
		[Tooltip("The amount of points the player starts with.")]
		public int StartingPoints;
		
		/// <summary>
		/// The current amount of points the player has.
		/// </summary>
		[Tooltip("The current amount of points the player has.")]
		public int CurrentPoints;

		private void Setup()
		{
			CurrentPoints = StartingPoints;
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