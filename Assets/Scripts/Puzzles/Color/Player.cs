﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// This player class is unique to the Color puzzle, and follows
	/// all the lines that are available in the current node if specified by
	/// the queue.
	/// </summary>
	public class Player : MonoBehaviour
	{
		enum State
		{ 
			Idle,
			Navigating,
			Moving,
			Waiting,
			Stuck,
			Done,
		}

		private Node currentNode;
		private PlayerPathFinding playerPathFinding;
		private ColorPuzzleSettings colorPuzzleSettings;
		private ICommandQueue<ColorCommand> commandQueue;

		private bool started;
		private State currentState = State.Idle; 

		private void Awake()
		{
			playerPathFinding = GetComponent<PlayerPathFinding>();
			colorPuzzleSettings = Resources.Load<ColorPuzzleSettings>("ColorPuzzleSettings");
		}

		public void StartNavigationQueue(ICommandQueue<ColorCommand> navigationQueue)
		{
			commandQueue = navigationQueue;
			started = true;

			StartCoroutine(RunQueue(OnRunCompleted));
		}

		private Line Navigate(ColorCommand colorCommand) => playerPathFinding.FindLine(colorCommand, currentNode);

		private void OnRunCompleted()
		{
			Debug.Log($"I AM DONE, ENDED WITH STATE: {currentState}");
		}

		private IEnumerator RunQueue(Action callback)
		{
			ColorCommand colorCommand = commandQueue.RequestNext();
			Line currentLine = null;

			bool running = true;

			while(running)
			{
				switch (currentState)
				{
					case State.Idle:
						currentState = State.Navigating;
						break;
					case State.Navigating:
						currentLine = Navigate(colorCommand);
						currentState = currentLine == null ? State.Stuck : State.Moving;
						break;
					case State.Moving:
						yield return Moving(currentLine);
						break;
					case State.Waiting:
						yield return new WaitForSeconds(colorPuzzleSettings.DestinationTimeout);
						break;
					case State.Stuck:
						running = false;
						break;
					case State.Done:
						running = false;
						break;
					default:
						break;
				}

				if (commandQueue.Empty)
				{
					break;
				}

				yield return new WaitForEndOfFrame();
			}

			callback?.Invoke();
		}

		private IEnumerator Moving(Line line)
		{
			Node endNode = currentNode == line.StartingNode ? line.EndNode : line.StartingNode;
			bool moving = true;
			while (moving)
			{
				Vector3 nextPosition = Vector3.MoveTowards(
					transform.position, 
					endNode.transform.position, 
					colorPuzzleSettings.PlayerSpeed * Time.deltaTime
					);

				transform.position = nextPosition;

				float distance = Vector3.Distance(transform.position, endNode.transform.position);
				if (Mathf.Approximately(distance, 0))
				{
					moving = false;
				}

				yield return new WaitForEndOfFrame();
			}
		}
	}
}
