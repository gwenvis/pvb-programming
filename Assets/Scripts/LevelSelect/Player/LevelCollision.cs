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
		private DN.LevelData levelData;

		private GameObject currentLevelSelected;

		private KeyCode enterLevelInput = KeyCode.Return;

		private void Update()
		{
			if (Input.GetKeyDown(enterLevelInput) && txtPanel.active)
			{
				levelLoader.SetLoadingLevelData(currentLevelSelected, selectedPuzzle, selectedAnimal, levelData);
				ServiceLocator.Locate<LevelMemoryService>().SetBiomeAndLevelAndAudioController(biomeController, levelLoader, audioListener);
				levelLoader.LoadInBetweenScene();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			var levelDataComponent = other.GetComponent<LevelData>();
			if (levelDataComponent)
			{
				currentLevelSelected = other.gameObject;
				selectedPuzzle = levelDataComponent.PuzzleSelected;
				selectedAnimal = levelDataComponent.AnimalSelected;
				levelData = levelDataComponent.Level;
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
