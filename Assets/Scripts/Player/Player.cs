using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Component[] components;
	public enum Move { Forward, TurnLeft, TurnRight };
	public List<Move> moveQueue;
	private bool isPlaying = false;
	float time = 1;
	private int dir = 0;
	private Vector2 gridPosition;
	private LevelCreator levelCreator;
	public PlayLevel playLevel;
	
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayLevel.playGame && !isPlaying)
		{
			isPlaying = true;
			getMoves();
			StartCoroutine(Play(0.5f));
		}
    }

	private IEnumerator Play(float seconds)
	{
		foreach (Move move in moveQueue)
		{
			if (move == Move.Forward)
			{
				WalkForward();
			}
			else
			{
				Turn(move);
			}
			yield return new WaitForSeconds(seconds);
			if(playLevel.gameEnded)
			{
				break;
			}
		}
		PlayLevel.playGame = false;
		isPlaying = false;
	}

	private void WalkForward()
	{
		gridPosition += GetDirection();
		Debug.Log(gridPosition);
		transform.position = new Vector2(0.5f + gridPosition.y + levelCreator.generateStartingPos.position.y,
					0.5f - gridPosition.x + levelCreator.generateStartingPos.position.x);
		if (levelCreator.Level[(int)gridPosition.x][(int)gridPosition.y] == 2)
		{
			playLevel.Won();
		}
		else if(levelCreator.Level[(int)gridPosition.x][(int)gridPosition.y] == 0)
		{
			playLevel.GameOver();
		}
		Debug.Log("WalkForward");
	}

	private void Turn(Move direction)
	{
		if(direction == Move.TurnLeft)
		{
			dir--;
			if(dir < 0)
			{
				dir = 3;
			}
			transform.Rotate(new Vector3(0, 0, 90f));
		}
		else
		{
			dir++;
			if(dir > 3)
			{
				dir = 0;
			}
			transform.Rotate(new Vector3(0, 0, -90f));
		}
		Debug.Log(dir);
		Debug.Log(direction.ToString());
	}

	private Vector2 GetDirection()
	{
		if(dir == 1)
		{
			return new Vector2(1, 0);
		}
		else if(dir == 2)
		{
			return new Vector2(0, -1);
		}
		else if(dir == 3)
		{
			return new Vector2(-1, 0);
		}
		else
		{
			return new Vector2(0, 1);
		}
	}

	private void getMoves()
	{
		foreach (DraggableBlocks draggableBlock in components)
		{
			if (draggableBlock.GetType() == typeof(TurnBlock))
			{
				TurnBlock turnBlock = draggableBlock as TurnBlock;
				if (turnBlock.dropdown.options[turnBlock.dropdown.value].text == "Left")
				{
					moveQueue.Add(Move.TurnLeft);
				}
				else
				{
					moveQueue.Add(Move.TurnRight);
				}
			}
			else if (draggableBlock.GetType() == typeof(MoveForwardBlock))
			{
				moveQueue.Add(Move.Forward);
			}
		}
	}

	public void SetPlayer(int x, int y, Vector3 position, LevelCreator levelCreator)
	{
		gridPosition = new Vector2(x, y);
		transform.position = position;
		this.levelCreator = levelCreator;
	}
}
