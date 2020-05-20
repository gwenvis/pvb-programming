using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// This player class is unique to the Color puzzle, and follows
	/// all the lines that are available in the current node if specified by
	/// the queue.
	/// </summary>
	public partial class Player : MonoBehaviour
	{
		public static event Action<State> RunFinishedEvent;

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

		public void SetStartingNode(Node node, bool moveToNode)
		{
			currentNode = node;

			if (moveToNode)
				transform.position = node.transform.position;
		}

		private Line Navigate(ColorCommand colorCommand) => playerPathFinding.FindLine(colorCommand, currentNode)?.Owner;

		private void OnRunCompleted()
		{
			RunFinishedEvent?.Invoke(currentState);
		}

		private IEnumerator RunQueue(Action callback)
		{
			ColorCommand colorCommand;
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
						colorCommand = commandQueue.RequestNext();
						currentLine = Navigate(colorCommand);
						currentState = currentLine == null ? State.Stuck : State.Moving;
						break;
					case State.Moving:
						// ReSharper disable once PossibleNullReferenceException
						Node endNode = currentNode.Data == currentLine.Data.StartingNode
							? currentLine.Data.EndNode.Owner
							: currentLine.Data.StartingNode.Owner;
						yield return Moving(currentLine, endNode);
						currentNode = endNode;
						currentState = currentNode.Data.IsFinish ? State.Done : State.Waiting;
						break;
					case State.Waiting:
						yield return new WaitForSeconds(colorPuzzleSettings.DestinationTimeout);
						currentLine = null;
						currentState = State.Navigating;
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

				if (commandQueue.Empty && currentLine == null)
				{
					break;
				}

				yield return new WaitForEndOfFrame();
			}

			callback?.Invoke();
		}

		private IEnumerator Moving(Line line, Node endNode)
		{
			bool moving = true;
			while (moving)
			{
				var endNodePosition = endNode.transform.position;

				var position = transform.position;
				Vector3 nextPosition = Vector3.MoveTowards(
					position, 
					endNodePosition, 
					colorPuzzleSettings.PlayerSpeed * Time.deltaTime
					);

				position = nextPosition;
				transform.position = position;

				float distance = Vector3.Distance(position, endNodePosition);
				if (distance < 0.001f)
				{
					moving = false;
				}

				yield return new WaitForEndOfFrame();
			}
		}
	}
}
