using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace ZombieGame.MenuManager
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class LevelPost : MonoBehaviour
	{
		public string Level = "";
		
		private BoxCollider _collider;
		private SteamVR_TrackedObject _nearbyHand = null;
		private SteamVR_Controller.Device _nearbyController = null;

		private void Start()
		{
			_collider = GetComponent<BoxCollider>();
			_collider.isTrigger = true;
		}

		private void Update()
		{
			if (_nearbyHand != null &&
				_nearbyController != null &&
				_nearbyController.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) ||
			    _nearbyController.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad)
			    )
			{
				ZombieGame.MenuManager.MenuManager.Instance.LoadLevel(Level);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			_nearbyHand = other.GetComponent<SteamVR_TrackedObject>();
			_nearbyController = SteamVR_Controller.Input((int) _nearbyHand.index);
		}
		
		private void OnTriggerExit(Collider other)
		{
			if (_nearbyHand == other.GetComponent<SteamVR_TrackedObject>())
			{
				_nearbyHand = null;
				_nearbyController = null;
			}
		}
		
	}
}