using UnityEngine;

namespace DN.LevelSelect.SceneManagment
{
	/// <summary>
	/// Here is where the pasue menu is handled.
	/// </summary>
	public class PauseScreen : MonoBehaviour
	{
		[SerializeField] private GameObject pauseScreen;
		[SerializeField] private GameObject pauseButton;

		public void PauseScreenActivate(bool value)
		{
			SetButtons(value);
			FreezeGame(value);
		}

		public void PauseScreenDeActivate(bool value)
		{
			SetButtons(!value);
			FreezeGame(!value);
		}

		private void SetButtons(bool value)
		{
			pauseScreen.SetActive(value);
			pauseButton.SetActive(!value);
		}

		private void FreezeGame(bool isPaused)
		{
			Time.timeScale = isPaused ? 0f : 1f;
		}
	}
}
