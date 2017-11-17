using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
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
	public class Zombie : MonoBehaviour
	{
		public float Health;
		public float Damage;
		public float Speed;

		public ZombieTypes Type;
		
		public NavMeshAgent NavAgent;
		public Animator Animator;
		
		
		private void Start()
		{
			NavAgent = GetComponent<NavMeshAgent>();
			Animator = GetComponent<Animator>();
		}

		public virtual void Move()
		{
			
		}
		
		public virtual void Attack()
	}
}