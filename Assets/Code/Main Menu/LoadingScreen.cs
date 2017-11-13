using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ZombieGame.MenuManager
{
	public class LoadingScreen : MonoBehaviour
	{
		public Image LoadingBackground;
		public Image LoadingBarBackground;
		public Text LoadingBarBackgroundText;
		public Image LoadingBarForeground;
		public Text LoadingBarForegroundText;

		private string _levelName;
		
		private const int _maxWidthOfLoadingBar = 1350;
		
		public void Setup(string levelName)
		{
			_levelName = levelName;
		}

		public void UpdateBar(float normalizedValue)
		{
			float calculatedWidth = normalizedValue * 1350;
			LoadingBarForeground.GetComponent<RectTransform>().sizeDelta = new Vector2(calculatedWidth,12f);
			LoadingBarBackgroundText.text = "LOADING "+_levelName+" ["+Math.Round(normalizedValue * 100,2).ToString()+"]";
			LoadingBarForegroundText.text = "LOADING "+_levelName+" ["+Math.Round(normalizedValue * 100,2).ToString()+"]";
		}

		
	}
}
