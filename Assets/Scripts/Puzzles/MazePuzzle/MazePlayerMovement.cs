using DN.Puzzle.Maze.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace DN.Puzzle.Maze
{
	/// <summary>
	/// Class for player movement in maze puzzle.
	/// </summary>
	public class MazePlayerMovement : MonoBehaviour
	{
		public Vector2 currentPosition { get; private set; }
		public event Action LoseLifeEvent;
		public event Action WinEvent;
		[SerializeField] private GameObject wireObject;
		[SerializeField] private Sprite wireStraight;
		[SerializeField] private Sprite wireCorner;
		private Quaternion startRotation;
		private Dictionary<Vector2, GameObject> spawnedWires = new Dictionary<Vector2, GameObject>();
		private MazeBlocks[][] level;
		private Vector2 targetPosition = Vector2.zero;
		private Quaternion targetRotation = Quaternion.identity;
		private List<(MazeFunctions function, MazeDraggableItem item)> functionQueue;
		private Vector2 tileSize;
		private float time = 0.5f;
		private int direction = 0;
		private float zRotation = 0;
		private bool atEnd = false;
		private bool lostGame = false;

		private void Start()
		{
			LoseLifeEvent += RemoveWires;
		}

		private void ResetBools()
		{
			atEnd = false;
			lostGame = false;
		}

		private void RemoveWires()
		{
			foreach (KeyValuePair<Vector2, GameObject> wire in spawnedWires)
			{
				Destroy(wire.Value);
			}
			spawnedWires = new Dictionary<Vector2, GameObject>();
		}

		public async void StartLevel()
		{
			foreach ((MazeFunctions function, MazeDraggableItem item) function in functionQueue)
			{
				await UseFunction(function);
			}
			if (!atEnd)
				LoseLifeEvent?.Invoke();

			if (lostGame)
				ResetBools();
		}

		private async Task UseFunction((MazeFunctions function, MazeDraggableItem item) function)
		{
			switch (function.function)
			{
				case MazeFunctions.Forward:
					WalkForward();
					await WaitSeconds(0.5f);
					break;
				case MazeFunctions.TurnLeft:
					Turn(90);
					await WaitSeconds(0.5f);
					break;
				case MazeFunctions.TurnRight:
					Turn(-90);
					await WaitSeconds(0.5f);
					break;
				case MazeFunctions.UntilEnd:
					await LoopBlocks(function);
					break;
				case MazeFunctions.IfForward:
					await If(function);
					break;
				case MazeFunctions.IfLeft:
					await If(function);
					break;
				case MazeFunctions.IfRight:
					await If(function);
					break;
			}
		}

		private async Task LoopBlocks((MazeFunctions function, MazeDraggableItem item) function)
		{
			List<(MazeFunctions function, MazeDraggableItem item)> newQueue = function.item.GetQueue();
			int iterations = 0;
			while (!atEnd || !lostGame)
			{
				iterations++;
				foreach ((MazeFunctions function, MazeDraggableItem item) item in newQueue)
				{
					await UseFunction(item);
				}
				await Awaiters.EndOfFrame;
				if (iterations >= 100)
					break;
			}
			if (!atEnd)
				LoseLifeEvent?.Invoke();
		}

		private async Task If((MazeFunctions function, MazeDraggableItem item) function)
		{
			List<(MazeFunctions function, MazeDraggableItem item)> newQueue = IsPathInDirection(function.function) ? function.item.GetQueue() :
				(function.item as IfBlock).HasElse ? (function.item as IfBlock).GetElseQueue() :
				new List<(MazeFunctions function, MazeDraggableItem item)>();

			foreach ((MazeFunctions function, MazeDraggableItem item) item in newQueue)
			{
				await UseFunction(item);
			}
		}

		private GameObject CreateWire(Sprite sprite)
		{
			GameObject wire = Instantiate(wireObject, transform.parent);
			wire.GetComponent<Image>().sprite = sprite;
			return wire;
		}

		private bool IsPathInDirection(MazeFunctions function)
		{
			switch (function)
			{
				case MazeFunctions.IfForward:
					Vector2 forwardPosition = currentPosition + GetDirection(direction);
					if ((int)forwardPosition.y < 0 || (int)forwardPosition.x < 0 || (int)forwardPosition.y > 9 || (int)forwardPosition.x > 9)
					{
						return false;
					}
					MazeBlocks nextForwardBlock = level[(int)forwardPosition.y][(int)forwardPosition.x];
					return nextForwardBlock == MazeBlocks.Path || nextForwardBlock == MazeBlocks.End;
				case MazeFunctions.IfLeft:
					Vector2 leftPosition = currentPosition + GetDirection(GetDirectionNum(direction + 1));
					if ((int)leftPosition.y < 0 || (int)leftPosition.x < 0 || (int)leftPosition.y > 9 || (int)leftPosition.x > 9)
					{
						return false;
					}
					MazeBlocks nextLeftBlock = level[(int)leftPosition.y][(int)leftPosition.x];
					return nextLeftBlock == MazeBlocks.Path || nextLeftBlock == MazeBlocks.End;
				case MazeFunctions.IfRight:
					Vector2 rightPosition = currentPosition + GetDirection(GetDirectionNum(direction - 1));
					if ((int)rightPosition.y < 0 || (int)rightPosition.x < 0 || (int)rightPosition.y > 9 || (int)rightPosition.x > 9)
					{
						return false;
					}
					MazeBlocks nextRightBlock = level[(int)rightPosition.y][(int)rightPosition.x];
					return nextRightBlock == MazeBlocks.Path || nextRightBlock == MazeBlocks.End;
				default:
					return false;
			}
		}

		private async Task WaitSeconds(float seconds)
		{
			float t = 0f;
			while (t < seconds)
			{
				t += Time.deltaTime / seconds;
				transform.position = Vector3.Lerp(transform.position, targetPosition, t);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
				await Awaiters.EndOfFrame;
			}

			if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.None ||
			(int)currentPosition.y < 0 || (int)currentPosition.x < 0 || (int)currentPosition.y > 9 || (int)currentPosition.x > 9)
			{
				LoseLifeEvent?.Invoke();
				lostGame = true;
				atEnd = true;
			}

			if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.End)
			{
				atEnd = true;
				WinEvent?.Invoke();
			}
		}

		private void WalkForward()
		{
			Vector2 prevPos = currentPosition;
			GameObject wire = CreateWire(wireStraight);
			wire.transform.SetPositionAndRotation(GetPositionOnGrid(prevPos), gameObject.transform.rotation);
			if (!spawnedWires.ContainsKey(prevPos))
			{
				spawnedWires.Add(prevPos, wire);
			}
			else
			{
				Destroy(wire);
			}
			currentPosition += GetDirection(direction);
			targetPosition = GetPositionOnGrid(currentPosition);
		}

		public void SetStartPositionAndLevel(Vector2 startPos, MazeBlocks[][] newLevel)
		{
			currentPosition = startPos;
			transform.position = GetPositionOnGrid(startPos);
			targetPosition = GetPositionOnGrid(startPos);
			level = newLevel;
		}

		public void SetStartRotation()
		{
			startRotation = transform.rotation;
		}

		public void Turn(int dir)
		{
			targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + dir));
			direction = dir == 90 ? direction + 1 : direction - 1;

			direction = GetDirectionNum(direction);

			GameObject wire = CreateWire(wireCorner);
			float additionalRotation = dir == 90 ? -90 : 0;
			wire.transform.SetPositionAndRotation(GetPositionOnGrid(currentPosition), Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + additionalRotation)));
			if (!spawnedWires.ContainsKey(currentPosition))
			{
				spawnedWires.Add(currentPosition, wire);
			}
			else
			{
				Destroy(wire);
			}
		}

		private int GetDirectionNum(int dir)
		{
			if (dir > 3)
				dir = 0;

			if (dir < 0)
				dir = 3;

			return dir;
		}

		private Vector2 GetDirection(int direction)
		{
			switch (direction)
			{
				case 0:
					return new Vector2(1, 0);
				case 1:
					return new Vector2(0, -1);
				case 2:
					return new Vector2(-1, 0);
				case 3:
					return new Vector2(0, 1);
			}
			return Vector2.zero;
		}

		public void SetToFirstRotation()
		{
			transform.rotation = startRotation;
		}

		public void SetDirection(int dir)
		{
			direction = dir;
			targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0));
		}

		public void SetMoveQueue(List<(MazeFunctions function, MazeDraggableItem item)> queue)
		{
			functionQueue = queue;
		}

		public void SetTileSize(Vector2 size)
		{
			tileSize = size;
		}

		private Vector3 GetPositionOnGrid(Vector2 position)
		{
			return new Vector3(
						transform.parent.position.x + position.x * tileSize.x,
						transform.parent.position.y - position.y * tileSize.y,
						1.0f);
		}
	}
}
