using DN.LevelSelect;
using DN.LevelSelect.SceneManagment;
using DN.Puzzle.Color;
using DN.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelLoader = DN.LevelSelect.SceneManagment.LevelLoader;

namespace DN.UI
{
	/// <summary>
	/// Controlls the win and lose screen
	/// </summary>
	public class WinLoseScreenController : MonoBehaviour
	{
		[SerializeField] private Lives lives;
		[SerializeField] private GameObject loseScreen;
		[SerializeField] private GameObject winScreen;
		[SerializeField] private WinController winController;

		[SerializeField] private LevelLoader levelLoader;

		private void Start()
		{
			levelLoader = ServiceLocator.Locate<LevelMemoryService>().LevelLoader;

			loseScreen.SetActive(false);
			winScreen.SetActive(false);
			lives.AllLifeLost += OnAllLifeLost;
			winController.GameWonEvent += OnGameWonEvent;
		}

		private void OnEnable()
		{
			Player.RunFinishedEvent += OnRunFinishedEvent;
		}

		private void OnDisable()
		{
			Player.RunFinishedEvent -= OnRunFinishedEvent;
		}

		private void OnRunFinishedEvent(Player.State obj)
		{
			if (obj == Player.State.Done)
			{
				OnGameWonEvent();
				return;
			}
			Stuck();
		}

		private void OnGameWonEvent()
		{
			winScreen.SetActive(true);
		}

		private void Stuck()
		{
			lives.LoseLife();
			ServiceLocator.Locate<LivesService>().SetCurrentLives(lives.CurrentLives, true);
			if (ServiceLocator.Locate<LivesService>().CurrenlivesLives >= 1)
			{
				print("Test");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
			}
		}

		private void OnAllLifeLost()
		{
			ServiceLocator.Locate<LivesService>().SetCurrentLives(3, false);
			loseScreen.SetActive(true);
		}

		public void ReloadScene()
		{
			ServiceLocator.Locate<LivesService>().SetCurrentLives(3, false);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Additive);
		}

		public void ContinueToLevelselectLose()
		{
			ServiceLocator.Locate<LivesService>().SetCurrentLives(3, false);
			levelLoader.LoadLevelSelectFromPuzzle(false);
		}

		public void ContinueToLevelselectWon()
		{
			levelLoader.LoadLevelSelectFromPuzzle(true);
		}
	}
}
