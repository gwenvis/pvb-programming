using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class PlayMazeLevel : MonoBehaviour
	{
		[SerializeField] private LoadLevel loadLevel;
		[SerializeField] private MazeDropZone dropZone;
		[SerializeField] private RunButton runButton;
		[SerializeField] private GameObject playerPrefab;
		[SerializeField] public MazePlayerMovement Player { get; private set; }

		private void Awake()
		{
			loadLevel.LevelLoadedEvent += OnLevelLoadedEvent;
			runButton.RunPuzzleEvent += OnRunPuzzleEvent;
		}

		private void OnRunPuzzleEvent()
		{
			(int childCount, MazeDraggableItem DraggableItem) startItem = dropZone.GetGameObjectWithMostChilds();
			MazeDraggableItem currentItem = startItem.DraggableItem;
			for (int i = 0; i < startItem.childCount; i++)
			{
				currentItem.GetComponent<IMovePlayerBlock>().MovePlayer(Player);
			}
		}

		private void OnLevelLoadedEvent()
		{
			Player = Instantiate(playerPrefab, transform.parent).GetComponent<MazePlayerMovement>();
			Player.SetPosition((int)loadLevel.StartPosition.x, (int)loadLevel.StartPosition.y);
		}

		private void OnDestroy()
		{
			loadLevel.LevelLoadedEvent -= OnLevelLoadedEvent;
		}
	}
}
