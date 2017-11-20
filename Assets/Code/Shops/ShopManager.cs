using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieGame.Shops
{
	public class ShopManager : MonoBehaviour
	{
		public static ShopManager Instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				return null;
			}
		}
		private static ShopManager _instance;
		private void Awake()
		{
			if (_instance != null)
				Destroy(this);
			_instance = this;
		}
		
		public Transform ItemSpawnPoint;
		public GameObject[] Rows;
		public Transform ItemUIGrid;
		private List<ShopRow> ITemUIRowInstances = new List<ShopRow>();

		private void Start()
		{
			Setup();
		}

		public void Setup()
		{
			foreach (GameObject row in Rows)
			{
				GameObject tmp = Instantiate(row,ItemUIGrid);
				ShopRow tmpRow = tmp.GetComponent<ShopRow>();
				ITemUIRowInstances.Add(tmpRow);
			}
		}
	}
}