using DN.LevelSelect.SceneManagment;
using DN.Puzzle.Color;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class CheckForWin : MonoBehaviour
	{
		[SerializeField] private LevelLoader levelLoader;

		private void OnEnable()
		{
			Player.RunFinishedEvent += OnRunFinishedEvent;
		}

		private void OnDisable()
		{
			Player.RunFinishedEvent -= OnRunFinishedEvent;
		}

		private void OnRunFinishedEvent(Player.State obj)
		{
			if(obj == Player.State.Done)
			{
				levelLoader.LoadLevelSelectFromPuzzle(true);

				return;
			}
			levelLoader.LoadLevelSelectFromPuzzle(false);
		}
	}
}
