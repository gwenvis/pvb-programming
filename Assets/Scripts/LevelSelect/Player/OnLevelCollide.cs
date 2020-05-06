using DN.Levelselect.LevelData;
using DN.SceneManagement;
using DN.SceneManagement.Data;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class OnLevelCollide : MonoBehaviour
	{
		[SerializeField] private GameObject txtPanel;

		[SerializeField] private LevelLoader levelLoader;

		private string levelIndex;

		private void Update()
		{
			if (Input.GetKey(KeyCode.KeypadEnter) && txtPanel.active)
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
