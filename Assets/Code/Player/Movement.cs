using System.Collections;
using System.Collections.Generic;
using UnityEngine;	
using UnityEngine.AI;	
using Valve.VR;

namespace ZombieGame.Player
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Movement : MonoBehaviour
	{
		public static Movement Instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				return null;
			}
		}
		private static Movement _instance;
		private NavMeshAgent _navAgent;	
		
		private void Awake()
		{
			if (_instance != null)
				Destroy(this);
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>();
			return;
		}

		private void Update()
		{
			if (PlayerManager.Instance.LeftController.GetTouch(EVRButtonId.k_EButton_SteamVR_Touchpad))
			{
				Vector2 input = PlayerManager.Instance.LeftController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
				_navAgent.Move(transform.GetChild(3).forward * (input.y * Time.deltaTime) + transform.GetChild(3).right * (input.x * Time.deltaTime));
			}
			if (PlayerManager.Instance.RightController.GetTouch(EVRButtonId.k_EButton_SteamVR_Touchpad))
			{
				Vector2 input = PlayerManager.Instance.RightController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
				_navAgent.Move(transform.GetChild(3).forward * (input.y * Time.deltaTime) + transform.GetChild(3).right * (input.x * Time.deltaTime));
			}
		}
	}
}