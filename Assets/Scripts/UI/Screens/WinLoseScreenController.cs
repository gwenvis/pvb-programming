using DN.LevelSelect;
using DN.LevelSelect.SceneManagment;
using DN.Puzzle.Color;
using DN.Service;
using DN.Tutorial;
using System.Collections;
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
		[SerializeField] private Puzzle.Color.LevelLoader colorPuzzleLevelLoader;

		[SerializeField] private UIAssistant assistant;

		private LevelLoader levelLoader;

		private string puzzleName;

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
			if (colorPuzzleLevelLoader) colorPuzzleLevelLoader.Clean();

			lives.LoseLife();
			var livesService = ServiceLocator.Locate<LivesService>();
			livesService.SetCurrentLives(lives.CurrentLives, true);

			if (livesService.CurrentLives <= 0) return;
			
			if(colorPuzzleLevelLoader) colorPuzzleLevelLoader.LoadWithExistingData();
		}

		private IEnumerator Reload(Scene prevScene, string c)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Additive);
			yield return new WaitForSeconds(.1f);
			SceneManager.UnloadScene(prevScene);
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(c));
		}

		private void OnAllLifeLost()
		{
			ResetLives();
			loseScreen.SetActive(true);
		}

		public void ReloadScene()
		{
			ResetLives();
			Scene prevScene = SceneManager.GetActiveScene();
			puzzleName = SceneManager.GetActiveScene().name;
			StartCoroutine(Reload(prevScene, puzzleName));
		}

		public void ContinueToLevelselectLose()
		{
			ResetLives();
			levelLoader.LoadLevelSelectFromPuzzle(false);
		}

		public void ContinueToLevelselectWon()
		{
			ResetLives();
			levelLoader.LoadLevelSelectFromPuzzle(true);
		}

		private void ResetLives()
		{
			ServiceLocator.Locate<LivesService>().SetCurrentLives(3, false);
		}
	}
}
