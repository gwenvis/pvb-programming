using System;
using System.Collections.Generic;
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
		public event Action LevelLoadedEvent;
		[SerializeField]private Sprite[] blocks;
		[SerializeField]private GameObject canvas;
		[SerializeField] private Dictionary<MazeBlocks, Sprite> blockDictionary = new Dictionary<MazeBlocks, Sprite>();


		public static int MaxBlocks { get; private set; } = 20; 
		//Voor nu het level moet later gezet worden door data met SetLevel(); 
		public static MazeBlocks[][] Level { get; private set; } = {
										new[] { MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None },
										new[] { MazeBlocks.None, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.Path },
										new[] { MazeBlocks.None, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.Path },
										new[] { MazeBlocks.Start, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.Path, MazeBlocks.Path, MazeBlocks.Path },
										new[] { MazeBlocks.None, MazeBlocks.Path, MazeBlocks.Path, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.End },
										new[] { MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.Path, MazeBlocks.None, MazeBlocks.None },
										new[] { MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None, MazeBlocks.None },
										};

		private void Start()
		{
			for (int i = 0; i < Enum.GetValues(typeof(MazeBlocks)).Length; i++)
			{
				blockDictionary.Add(((MazeBlocks[])Enum.GetValues(typeof(MazeBlocks)))[i], blocks[i]);
			}
			Load(Level);
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
				for (int x = 0; x < mazeBlocks[x].GetLength(0); x++)
				{
					GameObject gameObject = new GameObject($"level tile ({x} {y})");
					gameObject.transform.SetParent(transform);
					gameObject.AddComponent<CanvasRenderer>();
					AspectRatioFitter af = gameObject.AddComponent<AspectRatioFitter>();
					af.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
					var sprite = gameObject.AddComponent<Image>();
					Sprite spriteee = blockDictionary[mazeBlocks[y][x]];
					sprite.sprite = spriteee;
					gameObject.transform.position = new Vector3(
						transform.position.x + x * 75,
						transform.position.y - y * 75,
						1.0f);
					gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
					if(mazeBlocks[y][x] == MazeBlocks.Start)
						StartPosition = new Vector2(x,y);
					if(mazeBlocks[y][x] == MazeBlocks.End)
						EndPosition = new Vector2(x, y);

				}
			}
		}
	}
}
