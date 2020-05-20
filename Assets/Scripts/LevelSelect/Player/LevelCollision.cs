using DN.LevelSelect.SceneManagment;
using DN.Service;
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
		[SerializeField] private BiomeController biomeController;
		[SerializeField] private SetAudioListener audioListener;

		private LevelData.SelectedPuzzle selectedPuzzle;
		private LevelData.SelectedAnimal selectedAnimal;

		private GameObject currentLevelSelected;

		private KeyCode enterLevelInput = KeyCode.Return;

		private void Update()
		{
			if (Input.GetKeyDown(enterLevelInput) && txtPanel.active)
			{
				levelLoader.SetLoadingLevelData(currentLevelSelected, selectedPuzzle, selectedAnimal);
				ServiceLocator.Locate<LevelMemoryService>().SetBiomeAndLevelAndAudioController(biomeController, levelLoader, audioListener);
				ServiceLocator.Locate<LevelMemoryService>().SetSelectedPuzzle(selectedPuzzle);
				levelLoader.LoadInBetweenScene();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<LevelData>())
			{
				currentLevelSelected = other.gameObject;
				selectedPuzzle = other.GetComponent<LevelData>().PuzzleSelected;
				selectedAnimal = other.GetComponent<LevelData>().AnimalSelected;
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
