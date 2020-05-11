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

		private LevelData.SelectedPuzzle selectedPuzzle;
		private LevelData.SelectedAnimal selectedAnimal;

		private GameObject otherGo;

		private bool isLocked;

		private KeyCode enterLevelInput = KeyCode.KeypadEnter;

		private void Update()
		{
			if (Input.GetKeyDown(enterLevelInput) && txtPanel.active)
			{
				levelLoader.SetLoadingLevelData(otherGo, selectedPuzzle, selectedAnimal, isLocked);
				levelLoader.LoadInBetweenScene();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<LevelData>())
			{
				otherGo = other.gameObject;
				selectedPuzzle = other.GetComponent<LevelData>().PuzzleSelected;
				selectedAnimal = other.GetComponent<LevelData>().AnimalSelected;
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
