using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using VRWeapons;

namespace ZombieGame
{
	public class Health : MonoBehaviour, IAttackReceiver
	{
		public float MaxHealth; 
		
		public float CurrentHealth;

		private bool _poisioned;
		private float _poisionDamage;
		private float _poisionTickRate;
		private float _poisionDuration;
				
		public UnityEvent OnAttackReceived;
		
		public void ReceiveAttack(VRWeapons.Weapon.Attack attack)
		{
			if (CurrentHealth - (attack.damage) > 0)
			{
				TakeHealth(attack.damage);
				OnAttackReceived.Invoke();
				return;
			}
			else
			{
				CurrentHealth = 0;
				Death();
				return;
			}
		}

		
		public virtual void RecieveDamage(float damage)
		{
			if (CurrentHealth - damage > 0)
			{
				print(damage);
				TakeHealth(damage);
				OnAttackReceived.Invoke();
				return;
			}
			else
			{
				CurrentHealth = 0;
				Death();
				return;
			}
		}

		public virtual void PickupHealth(float amount)
		{
			GiveHealth(amount);
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
			CurrentHealth = CurrentHealth + amount;
			if (CurrentHealth > MaxHealth)
			{
				CurrentHealth = MaxHealth;
			}
			return;
		}

		private void TakeHealth(float damage)
		{
			print(CurrentHealth - damage);
			CurrentHealth = CurrentHealth - damage;
			
			return;
		}

		private float _poisionLoopTimeTracker = 0;		
		private IEnumerator PoisionLoop()
		{
			while (_poisioned)
			{
				yield return new WaitForSeconds(_poisionTickRate);
				RecieveDamage( _poisionDamage );
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
