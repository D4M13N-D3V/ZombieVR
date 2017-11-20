using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using ZombieGame.Player;

namespace ZombieGame.Enemys
{


	public enum ZombieTypes
	{
		Slow,
		Normal,
		Spaztic
	}

	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class Enemy : MonoBehaviour
	{
		public EnemyProfile Profile;

		[NonSerialized]
		public EnemyHealth Health;
		
		private NavMeshAgent _navAgent;
		
		private Animator _animator;
		
		private bool _moving;
		
		private bool _attacking;
		
		private Health _target;

		//Setup inital values
		private void Awake()
		{
			Health = GetComponent<EnemyHealth>();
			Health.MaxHealth = Profile.Health;
			Health.CurrentHealth = Profile.Health;
			
			_navAgent = GetComponent<NavMeshAgent>();
			_navAgent.acceleration = Profile.Speed;
			_navAgent.speed = Profile.Speed;
			//_navAgent.angularSpeed = Profile.Speed;
			
			_animator = GetComponent<Animator>();
			_animator.runtimeAnimatorController = Profile.AnimController;
			_animator.SetInteger("MovementType",(int)Profile.Type);
		}

		//Start attacking player on start.
		private void Start()
		{
			Attack(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>());
		}
		
		/// <summary>
		/// Move to a transform.
		/// </summary>
		/// <param name="target">The transform to move to.</param>
		public virtual void Move(Transform target)
		{
			_moving = true;
			_navAgent.stoppingDistance = Profile.AttackDistance;
			StartCoroutine(MoveLoop(target));
		}

		//Handle cahsing target and initiating the attack loop.
		IEnumerator MoveLoop(Transform target)
		{
			NavMeshHit hit;
			if (NavMesh.SamplePosition(target.position, out hit, 10.0f, NavMesh.AllAreas))
			{
				_navAgent.destination = hit.position;
			}
			yield return new WaitForSeconds(1);
			while (_navAgent.remainingDistance>_navAgent.stoppingDistance)
			{
				if (_moving == false)
				{
					_navAgent.isStopped = true;
				}
				
				if (NavMesh.SamplePosition(target.position, out hit, 10.0f, NavMesh.AllAreas))
				{
					_navAgent.destination = hit.position;
				}
				yield return new WaitForSeconds(1);
			}
			_moving = false;
			if (_attacking == true)
			{
				StartCoroutine(AttackLoop());
			}
		}

		/// <summary>
		/// Start chasing player
		/// </summary>
		/// <param name="target">What to attack (requires a health system to be attatched).</param>
		public virtual void Attack(Health target)
		{
			if (target.GetComponent<Health>() != null)
			{
				_attacking = true;
				_target = target;
				Move(target.transform);
			}
		}

		IEnumerator AttackLoop()
		{
			_animator.SetBool("Attacking",true);
			while (_attacking==true && _moving==false)
			{
				_target.GetComponent<Health>().RecieveDamage(Profile.Damage);
				yield return new WaitForSeconds(Profile.AttackSpeed);
				if (_target == null || _target.GetComponent<PlayerHealth>().CurrentHealth<=0)
				{
					_animator.SetBool("Attacking",false);
					_attacking = false;
				}
				if (Vector3.Distance(_target.transform.position, transform.position)>Profile.AttackDistance)
				{
					print("Test");	
					_animator.SetBool("Attacking",false);
					Attack(_target);
					break;
				}
				
				if (_attacking == false)
				{
					_animator.SetBool("Attacking",false);
					break;
				}
			}
		}

		public virtual void Stop()
		{
			_attacking = false;
			
		}

	}
}