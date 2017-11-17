using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieGame;

namespace ZombieGame.Enemys
{
	public class EnemyHealth : Health
	{
		public EnemyHealth()
		{
				
		}
		
		public override void Death()
		{
			Destroy(gameObject);
		}
	}
}