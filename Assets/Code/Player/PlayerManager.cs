using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZombieGame.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance 
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return null;
            }
        }
        private static PlayerManager _instance;
        private void Awake()
        {
            if (_instance != null)
                Destroy(this);
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public SteamVR_TrackedObject LeftHand;
        public SteamVR_Controller.Device LeftController;
        public SteamVR_TrackedObject RightHand;
        public SteamVR_Controller.Device RightController;
        public Health Health;

        public void Setup()
        {
            gameObject.AddComponent<Health>();
            Health.Instance.MaxHealth = ZombieGame.GameManager.Instance.MaximumHealth;
            Health.Instance.MaxArmour = ZombieGame.GameManager.Instance.MaximumArmour;
            Health.Instance.Setup();
            
            gameObject.AddComponent<Movement>();
            
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
