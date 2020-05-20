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
		private List<MazeFunctions> functionQueue;
		private float time = 0.5f;
		private int direction = 0;
		private float zRotation = 0;

		public IEnumerator StartLevel()
		{
			bool gameEnded = false;
			foreach(MazeFunctions function in functionQueue)
			{
				UseFunction(function);
				float t = 0f;
				while (t < time)
				{
					t += Time.deltaTime / time;
					transform.position = Vector3.Lerp(transform.position, targetPosition, t);
					transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
					yield return null;
				}

				if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.None)
				{
					LoseLifeEvent?.Invoke();
					gameEnded = true;
					break;
				}

				if (level[(int)currentPosition.y][(int)currentPosition.x] == MazeBlocks.End)
					WinEvent?.Invoke();
			}
			if(!gameEnded)
				LoseLifeEvent?.Invoke();
		}

		private void UseFunction(MazeFunctions function)
		{
			switch(function)
			{
				case MazeFunctions.Forward:
					WalkForward();
					break;
				case MazeFunctions.TurnLeft:
					Turn(90);
					break;
				case MazeFunctions.TurnRight:
					Turn(-90);
					break;
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

		public void SetMoveQueue(List<MazeFunctions> queue)
		{
			functionQueue = queue;
		}

		private Vector3 GetPositionOnGrid(Vector2 startPos)
		{
			return new Vector3(
						transform.parent.position.x + 0.5f + startPos.x * 80,
						transform.parent.position.y + 0.5f - startPos.y * 80,
						1.0f);
		}
	}
}
