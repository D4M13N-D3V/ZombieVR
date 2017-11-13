using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieGame.Weapons
{
	public class Weapon
	{
		public string Name = "";
		public string Description = "";
		public int Price = 0;
		public int MaximumClips = 0;
		public int RoundsPerClip = 0;
		public bool SingleLoaded = false;
		public Vector3 HolsteredPosistion;
		public Vector3 EquipedPosistion;
	}	
	
	
}