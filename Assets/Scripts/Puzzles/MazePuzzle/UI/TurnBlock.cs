using UnityEngine;
using TMPro;
using System;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Controlls the TurnBlock
	/// </summary>
	public class TurnBlock : MazeDraggableItem, IMovePlayerBlock
	{
		[SerializeField] private TMP_Dropdown dropdown;
		MazeFunctions function = MazeFunctions.TurnLeft;

		protected override void Start()
		{
			base.Start();
			dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
			OnDropdownValueChanged();
		}

		private void OnDropdownValueChanged()
		{
			switch(dropdown.value)
			{
				case 0:
					function = MazeFunctions.TurnLeft;
					break;
				case 1:
					function = MazeFunctions.TurnRight;
					break;
			}
		}

		public MazeFunctions GetMazeFunction()
		{
			return function;
		}
	}
}
