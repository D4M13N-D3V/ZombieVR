using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieGame.Enemys
{
	[CreateAssetMenu(fileName = "Enemy Profile", menuName = "Zombie VR Game/Enemy Profile", order = 1)]
	public class EnemyProfile : ScriptableObject
	{
		public string Name;
		public float Health;
		public ZombieTypes Type;
		public float Speed;
		public float Damage;
		public float AttackDistance;
		public float AttackSpeed;
		public RuntimeAnimatorController AnimController;
	}
}