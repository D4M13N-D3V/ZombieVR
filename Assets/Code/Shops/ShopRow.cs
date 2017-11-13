using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieGame.Shops
{
	public class ShopRow : MonoBehaviour
	{
		public GameObject Weapon;
		public GameObject Ammo;
		public int WeaponCost;
		public int AmmoCost;
		public string WeaponLabel = "";
		public string AmmoLabel = "";
		public Text BuyWeaponText;
		public Text BuyAmmoText;
		public Button BuyWeaponButton;
		public Button BuyAmmoButton;
		
		public void BuyWeapon()
		{
			GameObject tmp = GameObject.Instantiate(Weapon);
			tmp.transform.position = ZombieGame.Shops.ShopManager.Instance.ItemSpawnPoint.transform.position;
		}

		public void BuyAmmo()
		{
			GameObject tmp = GameObject.Instantiate(Ammo);
			tmp.transform.position = ZombieGame.Shops.ShopManager.Instance.ItemSpawnPoint.transform.position;
		}

		public void Start()
		{
			BuyWeaponText.text = WeaponLabel+" ["+WeaponCost.ToString()+"]";
			BuyAmmoText.text =  AmmoLabel+" ["+AmmoCost.ToString()+"]";
			BuyWeaponButton.onClick.AddListener(BuyWeapon);
			BuyAmmoButton.onClick.AddListener(BuyAmmo);
		}

	}
}