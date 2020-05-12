using DN.LevelSelect.LevelData;
using DN.LevelSelect.SceneManagment;
using DN.SceneManagement.Data;
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

		private string levelIndex;

		private KeyCode enterLevelInput = KeyCode.KeypadEnter;

		private void Update()
		{
			if (Input.GetKey(enterLevelInput) && txtPanel.active)
			{
				levelLoader.LoadScene(levelIndex);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<LevelDataMap>())
			{
				levelIndex = other.GetComponent<LevelDataMap>().level.ToString();
				txtPanel.SetActive(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<LevelDataMap>())
			{
				txtPanel.SetActive(false);
			}
		}
	}
}
