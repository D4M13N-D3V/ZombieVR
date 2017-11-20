using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZombieGame.Player
{
    public class PManager : MonoBehaviour
    {
        public static PManager Instance 
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return null;
            }
        }
        private static PManager _instance;
        private void Awake()
        {
            if (_instance != null)
                Destroy(this);
            _instance = this;
        }
        
        public SteamVR_TrackedObject LeftHand;
        public SteamVR_Controller.Device LeftController;
        public SteamVR_TrackedObject RightHand;
        public SteamVR_Controller.Device RightController;

        public void Setup()
        {
            PlayerHealth.Instance.MaxHealth = GameManager.Instance.MaximumHealth;
            PlayerHealth.Instance.Setup();
            
            if (LeftHand != null)
            {    
                LeftController = SteamVR_Controller.Input((int) LeftHand.index);
            }
            if (RightHand != null)
            {
                RightController = SteamVR_Controller.Input((int) RightHand.index);
            }
        }
        
        
    }
}
