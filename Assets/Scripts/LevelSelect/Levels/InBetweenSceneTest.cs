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
		[SerializeField] private LevelLoader levelLoader;

		private void Update()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				levelLoader.isInBetweenFinished = true;
				levelLoader.LoadPuzzleScene();
			}
		}
	}
}
