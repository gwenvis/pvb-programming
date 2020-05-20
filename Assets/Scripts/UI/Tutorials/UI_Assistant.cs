using DN.LevelSelect;
using DN.LevelSelect.Player;
using DN.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DN.Tutorial
{
	/// <summary>
	/// This is where you check if tutorail is needed and activate the tutorial.
	/// </summary>
	public class UI_Assistant : MonoBehaviour
	{
		[SerializeField] private GameObject helperObj;

		[SerializeField] private GameObject controlsNext;
		[SerializeField] private GameObject beginTextObj;

		[SerializeField] private Vehicle vehicle;
		[SerializeField] private RawImage rawImage;
		[SerializeField] private VideoPlayer videoPlayer;

		[SerializeField] private string beginText;

		private const string LEVEL_SELECT_NAME = "LevelSelect";

		private Text messageText;

		private void Awake()
		{
			if (!ServiceLocator.Locate<LevelMemoryService>().IsMazeTutorialDone || !ServiceLocator.Locate<LevelMemoryService>().IsColorTutorialDone)
			{
				if (ServiceLocator.Locate<LevelMemoryService>().Assistant)
				{
					ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(true);
				}

				if (ServiceLocator.Locate<LevelMemoryService>().SelectedPuzzle == LevelData.SelectedPuzzle.ColorPuzzle && !ServiceLocator.Locate<LevelMemoryService>().IsColorTutorialDone)
				{
					ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(false);
					ServiceLocator.Locate<LevelMemoryService>().SetDoneOnceTutorialColor(true);
				}

				if (ServiceLocator.Locate<LevelMemoryService>().SelectedPuzzle == LevelData.SelectedPuzzle.MazePuzzle && !ServiceLocator.Locate<LevelMemoryService>().IsMazeTutorialDone)
				{
					ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(false);
					ServiceLocator.Locate<LevelMemoryService>().SetDoneOnceTutorialMaze(true);
				}
			}
			else
			{
				ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(true);
			}

			print(ServiceLocator.Locate<LevelMemoryService>().Assistant);

			if (vehicle != null)
			{
				vehicle.canDrive = false;
			}
			messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
		}

		private void Start()
		{
			if (!ServiceLocator.Locate<LevelMemoryService>().Assistant)
			{
				TextWriter.AddWrite_Static(
				messageText,
				beginText,
				.075f,
				true,
				controlsNext,
				beginTextObj,
				videoPlayer
				);
			}
			else
			{
				SetAllUIOff();
			}
		}

		public void SetAllUIOff()
		{
			if (vehicle != null)
			{
				vehicle.canDrive = true;
			}
			helperObj.SetActive(false);

			if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(LEVEL_SELECT_NAME))
			{
				ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(true);
			}
			else
			{
				ServiceLocator.Locate<LevelMemoryService>().SetTutorialStatus(false);
			}
		}
	}
}
