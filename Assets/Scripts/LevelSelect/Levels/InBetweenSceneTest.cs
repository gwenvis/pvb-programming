using DN.LevelSelect;
using DN.LevelSelect.SceneManagment;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class InBetweenSceneTest : MonoBehaviour
	{
		private LevelLoader levelLoader;

		private const string LEVEL_LOADER = "LevelManager";

		private void Update()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				print(GameObject.Find(LEVEL_LOADER).GetComponent<LevelLoader>().SelectedAnimal.ToString());
				//levelLoader = GameObject.Find(LEVEL_LOADER).GetComponent<LevelLoader>();
				//levelLoader.Debugman();
				//levelLoader.isInBetweenFinished = true;
				//levelLoader.LoadPuzzleScene();
			}
		}
	}
}
