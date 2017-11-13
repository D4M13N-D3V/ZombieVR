using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieGame.MenuManager;

namespace ZombieGame.MenuManager
{
	public class MenuManager : MonoBehaviour
	{
		public static MenuManager Instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				return null;
			}
		}
		private static MenuManager _instance;
		private void Awake()
		{
			if (_instance != null)
				Destroy(this);
			_instance = this;
			_loadingScreens = Object.FindObjectsOfType<LoadingScreen>();
		}
		
		
		private LoadingScreen[] _loadingScreens;
		private AsyncOperation LoadingOperation;
		
		public void LoadLevel(string levelName)
		{
			foreach (LoadingScreen loadingScreen in _loadingScreens)
			{
				loadingScreen.gameObject.SetActive(true);
			}
			StartCoroutine(LoadLevelAsynchronously(levelName));
		}

		private IEnumerator LoadLevelAsynchronously(string levelName)
		{
			LoadingOperation = SceneManager.LoadSceneAsync(levelName);

			while (!LoadingOperation.isDone == false)
			{
				foreach (LoadingScreen loadingScreen in _loadingScreens)
				{
					loadingScreen.UpdateBar(LoadingOperation.progress);
				}
			}
			yield return null;
		}
	}
}
