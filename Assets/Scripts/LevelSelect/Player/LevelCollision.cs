using DN.LevelSelect.SceneManagment;
using UnityEngine;

namespace DN.LevelSelect.Player
{
	/// <summary>
	/// This is where you check for collision with the car and the Level Pads and the input for entering the level
	/// </summary>
	public class LevelCollision : MonoBehaviour
	{
		[SerializeField] private GameObject txtPanel;

		[SerializeField] private LevelLoader levelLoader;

		private LevelData.SelectedPuzzle levelIndex;

		private GameObject otherGo;

		private bool isLocked;

		private KeyCode enterLevelInput = KeyCode.KeypadEnter;

		private void Update()
		{
			if (Input.GetKey(enterLevelInput) && txtPanel.active)
			{
				levelLoader.LoadScene(otherGo, levelIndex, isLocked);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<LevelData>())
			{
				otherGo = other.gameObject;
				levelIndex = other.GetComponent<LevelData>().PuzzleSelected;
				isLocked = other.GetComponent<LevelData>().isLocked;
				txtPanel.SetActive(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<LevelData>())
			{
				txtPanel.SetActive(false);
			}
		}
	}
}
