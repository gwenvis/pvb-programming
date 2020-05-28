using System;
using System.Collections.Generic;
using DN.LevelSelect;
using DN.Service;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for loading the levels
	/// </summary>
	public class LoadLevel : MonoBehaviour
	{
		public Vector2 StartPosition { get; private set; }
		public Vector2 EndPosition { get; private set; }
		public Vector2 TileSize { get; private set; }
		public event Action LevelLoadedEvent;
		[SerializeField] private Sprite[] blocks;
		[SerializeField] private GameObject canvas;
		[SerializeField] private Dictionary<MazeBlocks, Sprite> blockDictionary = new Dictionary<MazeBlocks, Sprite>();
		public static MazeBlocks[][] Level { get; private set; }
		public static int MaxBlocks { get; private set; }

		private void Start()
		{
			for (int i = 0; i < Enum.GetValues(typeof(MazeBlocks)).Length; i++)
			{
				blockDictionary.Add(((MazeBlocks[])Enum.GetValues(typeof(MazeBlocks)))[i], blocks[i]);
			}

			var service = ServiceLocator.Locate<LevelMemoryService>();

			var levelData = service.LevelData as MazeLevelData;

			if (levelData == null)
			{
				throw new Exception($"Level was invalid. Was acutally of type {service.LevelData?.GetType().Name}");
			}
			
			SetMaxBlocks(levelData.MaxBlocks);
			var mazeFromImage = levelData.GetMazeFromImage();
			SetLevel(mazeFromImage);
			Load(mazeFromImage);
			LevelLoadedEvent?.Invoke();
		}

		public void SetLevel(MazeBlocks[][] level)
		{
			Level = level;
		}

		public void SetMaxBlocks(int amount)
		{
			MaxBlocks = amount;
		}

		public void Load(MazeBlocks[][] mazeBlocks)
		{
			CreateLevel(mazeBlocks);
		}

		public void CreateLevel(MazeBlocks[][] mazeBlocks)
		{
			for (int y = 0; y < mazeBlocks.GetLength(0); y++)
			{
				for (int x = 0; x < mazeBlocks[y].GetLength(0); x++)
				{
					GameObject obj = new GameObject($"level tile ({x} {y})");
					obj.transform.SetParent(transform);
					obj.AddComponent<CanvasRenderer>();
					RectTransform t = obj.AddComponent<RectTransform>();
					UiObjectScaler objectScaler = obj.AddComponent<UiObjectScaler>();
					objectScaler.SetTransform(t);
					objectScaler.ChangeValues(new Vector2(0.1041f, 0.1041f), Vector2.zero, false);
					AspectRatioFitter af = obj.AddComponent<AspectRatioFitter>();
					af.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
					var sprite = obj.AddComponent<Image>();
					Sprite spriteee = blockDictionary[mazeBlocks[y][x]];
					sprite.sprite = spriteee;
					obj.transform.position = new Vector3(
						transform.position.x + x * obj.transform.GetComponent<RectTransform>().rect.width/2,
						transform.position.y - y * obj.transform.GetComponent<RectTransform>().rect.height/2,
						1.0f);
					obj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
					if (mazeBlocks[y][x] == MazeBlocks.Start)
					{
						StartPosition = new Vector2(x, y);
						TileSize = new Vector2(obj.transform.GetComponent<RectTransform>().rect.width / 2, obj.transform.GetComponent<RectTransform>().rect.height / 2);
					}
					if(mazeBlocks[y][x] == MazeBlocks.End)
						EndPosition = new Vector2(x, y);

				}
			}
		}
	}
}
