using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieGame;

namespace ZombieGame.Enemys
{
	public class EnemyHealth : Health
	{
		private void Awake()
		{
			
			Component[] components = GetComponentsInChildren(typeof(Transform));
        
			foreach (Component c in components)
			{
				if (c.GetComponent<Rigidbody>() != null)
				{
					c.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}

		public override void Death()
		{
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<Animator>().enabled = false;
			
			Component[] components = GetComponentsInChildren(typeof(Transform));
        
			foreach (Component c in components)
			{
				if (c.GetComponent<Rigidbody>() != null)
				{
					c.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
		}
	}
}