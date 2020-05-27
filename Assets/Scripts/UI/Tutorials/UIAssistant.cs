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
	public class UIAssistant : MonoBehaviour
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
			var service = ServiceLocator.Locate<LevelMemoryService>();

			if (!service.IsMazeTutorialDone || !service.IsColorTutorialDone)
			{
				if (service.Assistant)
				{
					service.SetTutorialStatus(true);
				}

				if (service.SelectedPuzzle == LevelDataEditor.SelectedPuzzle.ColorPuzzle && !service.IsColorTutorialDone)
				{
					service.SetTutorialStatus(false);
					service.SetDoneOnceTutorialColor(true);
				}

				if (service.SelectedPuzzle == LevelDataEditor.SelectedPuzzle.MazePuzzle && !service.IsMazeTutorialDone)
				{
					service.SetTutorialStatus(false);
					service.SetDoneOnceTutorialMaze(true);
				}
			}
			else
			{
				service.SetTutorialStatus(true);
			}

			if (vehicle != null)
			{
				vehicle.CanDrive = false;
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
				vehicle.CanDrive = true;
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
