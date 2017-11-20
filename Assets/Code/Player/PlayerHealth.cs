using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieGame;

namespace ZombieGame.Player
{
    public class PlayerHealth : Health
    {
        public static PlayerHealth Instance 
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return null;
            }
        }
        private static PlayerHealth _instance;
        private void Awake()
        {
            if (_instance != null)
                Destroy(this);
            _instance = this;
        }
		
        
        public virtual void Setup()
        {
            print("test");
            CurrentHealth = MaxHealth;
        }
		
        public override void Death()
        {
            SceneManager.LoadScene(0);
        }
    }
}