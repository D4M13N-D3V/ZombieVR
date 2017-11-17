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
        public override void Death()
        {
            SceneManager.LoadScene(0);
        }
    }
}