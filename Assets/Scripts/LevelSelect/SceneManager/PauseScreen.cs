using UnityEngine;

namespace DN.Levelselect.SceneManagment
{
	/// <summary>
	/// Here i handle all the pause menu stuff.
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
			if (isPaused)
			{
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}
}
