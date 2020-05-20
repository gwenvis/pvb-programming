using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DN.Puzzle.Maze.UI;
using DN.UI;

namespace DN.Puzzle.Maze
{
	/// <summary>
	/// Gets Puzzle Data and uses it to set player data.
	/// </summary>
	public class PlayMazeLevel : MonoBehaviour
	{
		public MazePlayerMovement Player { get; private set; }
		[SerializeField] private LoadLevel loadLevel;
		[SerializeField] private MazeDropZone dropZone;
		[SerializeField] private RunButton runButton;
		[SerializeField] private GameObject playerPrefab;
		[SerializeField] private Lives lives;
		[SerializeField] private WinController winController;
		[SerializeField] private TextMeshProUGUI blocktext;

		private void Awake()
		{
			loadLevel.LevelLoadedEvent += OnLevelLoadedEvent;
			runButton.RunPuzzleEvent += OnRunPuzzleEvent;
			SpawnBlock.ChangedBlocksEvent += OnChangedBlocksEvent;
			OnChangedBlocksEvent(SpawnBlock.SpawnedBlocks);
		}

		private void OnChangedBlocksEvent(int spawnedBlocks)
		{
			blocktext.text = LoadLevel.MaxBlocks - spawnedBlocks > 0 ? 
			$"Je kan nog {LoadLevel.MaxBlocks - spawnedBlocks} blokken plaatsen" :
			"Je kan geen blokken meer plaatsen";
		}

		private void OnRunPuzzleEvent()
		{
			(int childCount, MazeDraggableItem DraggableItem) startItem = dropZone.GetGameObjectWithMostChilds();
			MazeDraggableItem currentItem = startItem.DraggableItem;
			List< (MazeFunctions function, MazeDraggableItem item)> queue = new List<(MazeFunctions function, MazeDraggableItem item)>();
			for (int i = 0; i < startItem.childCount; i++)
			{
				queue.Add((currentItem.GetComponent<IMovePlayerBlock>().GetMazeFunctions(), currentItem));
				currentItem = currentItem.DropZoneHolder.GetComponent<BlockDropZone>().CurrentObj as MazeDraggableItem;
			}
			Player.SetMoveQueue(queue);
			StartCoroutine(Player.StartLevel());
		}

		private void OnLevelLoadedEvent()
		{
			Player = Instantiate(playerPrefab, transform).GetComponent<MazePlayerMovement>();
			Player.SetTileSize(loadLevel.TileSize);
			Player.SetStartPositionAndLevel(loadLevel.StartPosition, LoadLevel.Level);
			Player.LoseLifeEvent += OnLoseLifeEvent;
			Player.WinEvent += OnWinEvent;
		}

		private void OnWinEvent()
		{
			winController.WonGame();
			dropZone.DestroyAllBlocks();
		}

		private void OnLoseLifeEvent()
		{
			lives.LoseLife();
			dropZone.DestroyAllBlocks();
			Player.SetMoveQueue(null);
			Player.SetDirection(0);
			Player.SetStartPositionAndLevel(loadLevel.StartPosition, LoadLevel.Level);
		}

		private void OnDestroy()
		{
			loadLevel.LevelLoadedEvent -= OnLevelLoadedEvent;
			Player.LoseLifeEvent -= OnLoseLifeEvent;
			Player.WinEvent -= OnWinEvent;
		}
	}
}
