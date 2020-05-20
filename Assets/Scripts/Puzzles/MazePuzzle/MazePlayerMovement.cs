using DN.Puzzle.Maze.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		private MazeBlocks[][] level;
		private Vector2 targetPosition = Vector2.zero;
		private Quaternion targetRotation = Quaternion.identity;
		private List<(MazeFunctions function, MazeDraggableItem item)> functionQueue;
		private Vector2 tileSize;
		private float time = 0.5f;
		private int direction = 0;
		private float zRotation = 0;

		public IEnumerator StartLevel()
		{
			bool loopEnded = false;
			foreach((MazeFunctions function, MazeDraggableItem item) function in functionQueue)
			{
				Debug.Log(function.function);
				bool isInLoop = UseFunction(function);
				float t = 0f;

				if (isInLoop)
				{
					loopEnded = true;
					break;
				}

				while (t < time)
				{
					t += Time.deltaTime / time;
					transform.position = Vector3.Lerp(transform.position, targetPosition, t);
					transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
					yield return new WaitForEndOfFrame();
				}

				if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.None)
				{
					LoseLifeEvent?.Invoke();
					loopEnded = true;
					break;
				}

				if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.End)
				{
					loopEnded = true;
					WinEvent?.Invoke();
				}
			}
			if(!loopEnded)
				LoseLifeEvent?.Invoke();
		}

		private bool UseFunction((MazeFunctions function, MazeDraggableItem item) function)
		{
			switch(function.function)
			{
				case MazeFunctions.Forward:
					WalkForward();
					return false;
				case MazeFunctions.TurnLeft:
					Turn(90);
					return false;
				case MazeFunctions.TurnRight:
					Turn(-90);
					return false;
				case MazeFunctions.UntilEnd:
					StartCoroutine(LoopBlocks(function));
					return true;
				default:
					return false;
			}
		}

		private IEnumerator LoopBlocks((MazeFunctions function, MazeDraggableItem item) function)
		{
			int positionInList = functionQueue.IndexOf(function);
			bool atEnd = false;
			bool lostGame = false;
			List<(MazeFunctions function, MazeDraggableItem item)> newQueue = function.item.GetQueue();
			while (!atEnd || !lostGame)
			{
				foreach((MazeFunctions function, MazeDraggableItem item) item in newQueue)
				{
					UseFunction(item);
					float t = 0f;
					while (t < time)
					{
						t += Time.deltaTime / time;
						transform.position = Vector3.Lerp(transform.position, targetPosition, t);
						transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
						yield return new WaitForEndOfFrame();
					}

					if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.None)
					{
						LoseLifeEvent?.Invoke();
						lostGame = true;
						atEnd = true;
						break;
					}

					if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.End)
					{
						atEnd = true;
						WinEvent?.Invoke();
					}
				}
				yield return new WaitForEndOfFrame();
			}
		}

		private void WalkForward()
		{
			currentPosition += GetDirection();
			targetPosition = GetPositionOnGrid(currentPosition);
		}

		public void SetStartPositionAndLevel(Vector2 startPos, MazeBlocks[][] newLevel)
		{
			currentPosition = startPos;
			transform.position = GetPositionOnGrid(startPos);
			targetPosition = GetPositionOnGrid(startPos);
			level = newLevel;
		}

		public void Turn(int direction)
		{
			targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + direction));
			this.direction = direction == 90 ? this.direction += 1 : this.direction -= 1;

			if (this.direction > 3)
				this.direction = 0;

			if (this.direction < 0)
				this.direction = 3;
		}

		private Vector2 GetDirection()
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
