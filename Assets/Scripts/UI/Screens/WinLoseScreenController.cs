using DN.LevelSelect.SceneManagment;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			loseScreen.SetActive(false);
			winScreen.SetActive(false);
			lives.AllLifeLost += OnAllLifeLost;
			winController.GameWonEvent += OnGameWonEvent;
		}

		private void OnGameWonEvent()
		{
			winScreen.SetActive(true);
		}

		private void OnAllLifeLost()
		{
			loseScreen.SetActive(true);
		}

		public void ReloadScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		public void ContinueToLevelselectLose()
		{
			levelLoader.LoadLevelSelectFromPuzzle(false);
		}

		public void ContinueToLevelselectWon()
		{
			levelLoader.LoadLevelSelectFromPuzzle(true);
		}
	}
}
