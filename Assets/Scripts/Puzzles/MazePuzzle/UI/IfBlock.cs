using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for if blocks
	/// </summary>
	public class IfBlock : LoopBlockDraggableItem, IMovePlayerBlock
	{
		public bool HasElse { get; private set; }
		[SerializeField] protected LoopDropZone elseObject;
		[SerializeField] private TMP_Dropdown dropdown;
		private List<(MazeFunctions function, MazeDraggableItem item)> elseQueue;
		private MazeFunctions function = MazeFunctions.IfForward;

		protected override void Start()
		{
			base.Start();
			HasElse = elseObject != null;
			dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
			OnDropdownValueChanged();
		}

		private void OnDropdownValueChanged()
		{
			switch (dropdown.value)
			{
				case 0:
					function = MazeFunctions.IfForward;
					break;
				case 1:
					function = MazeFunctions.IfLeft;
					break;
				case 2:
					function = MazeFunctions.IfRight;
					break;
			}
		}

		public List<(MazeFunctions function, MazeDraggableItem item)> GetElseQueue()
		{
			elseQueue = new List<(MazeFunctions, MazeDraggableItem)>();
			foreach (MazeDraggableItem item in elseObject.GetChildObjects())
			{
				elseQueue.Add((item.GetComponent<IMovePlayerBlock>().GetMazeFunction(), item));
			}
			return elseQueue;
		}

		public MazeFunctions GetMazeFunction()
		{
			return function;
		}
	}
}
