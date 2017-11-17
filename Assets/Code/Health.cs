using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieGame
{
	public class Health : MonoBehaviour
	{
		public static Health Instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				return null;
			}
		}
		private static Health _instance;
		private void Awake()
		{
			if (_instance != null)
				Destroy(this);
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
		public float MaxHealth;
		private float _currentHealth;
		public float MaxArmour;
		private float _currentArmour;

		private bool _poisioned;
		private float _poisionDamage;
		private float _poisionTickRate;
		private float _poisionDuration;

		public virtual void Setup()
		{
			_currentHealth = MaxHealth;
			_currentArmour = MaxArmour;
		}
		
		public virtual void TakeDamage(float damage)
		{
			if ( damage-_currentArmour<=0 )
			{
				TakeArmour(damage);
				return;
			}
			else if (_currentHealth - (damage - _currentArmour) > 0)
			{
				TakeArmour(_currentArmour);
				TakeHealth(_currentHealth - (damage - _currentArmour));
				return;
			}
			else
			{
				return;
			}
		}

		public virtual void PickupHealth(float amount)
		{
			GiveHealth(amount);
			return;
		}

		public virtual void PickupArmour(float amount)
		{
			GiveArmour(amount);
			return;
		}

		public virtual void Poision(float damage, float durationInSeconds)
		{
			_poisioned = true;
			_poisionDuration = durationInSeconds;
			_poisionDamage = damage;
			_poisionTickRate = damage / durationInSeconds;
			StartCoroutine(PoisionLoop());
		}

		public virtual void Death()
		{
			print("DEAD");	
		}
		
		private void GiveHealth(float amount)
		{
			_currentHealth = _currentHealth + amount;
			if (_currentHealth > MaxHealth)
			{
				_currentHealth = MaxHealth;
			}
			return;
		}

		private void GiveArmour(float amount)
		{
			_currentArmour = _currentArmour + amount;
			if (_currentArmour > MaxArmour)
			{
				_currentArmour = MaxArmour;
			}
			return;
		}

		private void TakeArmour(float damage)
		{
			_currentArmour = _currentArmour - damage;
			return;
		}

		private void TakeHealth(float damage)
		{
			_currentHealth = _currentHealth - damage;
			return;
		}

		private float _poisionLoopTimeTracker = 0;		
		private IEnumerator PoisionLoop()
		{
			while (_poisioned)
			{
				yield return new WaitForSeconds(_poisionTickRate);
				TakeDamage( _poisionDamage );
				_poisionLoopTimeTracker = _poisionLoopTimeTracker + _poisionTickRate;
				if (_poisionLoopTimeTracker >= _poisionDuration)
				{
					_poisioned = false;
					_poisionDuration = 0;
					_poisionDamage = 0;
					_poisionTickRate = 0;
					_poisionLoopTimeTracker = 0;
					StopCoroutine(PoisionLoop());
				}
			}
		}
		
		
	}
}
