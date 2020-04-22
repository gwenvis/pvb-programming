using UnityEngine;

namespace DN.Puzzle.Color.UI
{
	/// <summary>
	/// The run button starts the queue when the button is pressed.
	/// </summary>
	public class RunButton : MonoBehaviour
	{
		// todo : fix this mess (shouldn't be like this and perhaps work with events instead)
		[SerializeField] private ColorCommandDropzone dropzone;
		[SerializeField] private Player player;
		[SerializeField] private UnityEngine.UI.Button button;

		private void OnEnable()
		{
			button.onClick.AddListener(OnButtonClickEvent);
		}

		private void OnDisable()
		{
			button.onClick.RemoveAllListeners();
		}

		private void OnButtonClickEvent()
		{
			player.StartNavigationQueue(new ColorCommandQueue(dropzone.CommandQueue));
		}
	}
}
