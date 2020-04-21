using UnityEngine;

namespace DN.UI.MainMenu
{
	/// <summary>
	/// Here i link the play button to activate Level select.
	/// </summary>
	public class MenuButtonsController : MonoBehaviour
	{
		[SerializeField] private GameObject levelSelect;
		[SerializeField] private GameObject playButton;
		[SerializeField] private GameObject backButton;

		public void SetLevelSelectActive(bool value)
		{
			levelSelect.SetActive(value);
			backButton.SetActive(value);
			playButton.SetActive(!value);
		}
	}
}
