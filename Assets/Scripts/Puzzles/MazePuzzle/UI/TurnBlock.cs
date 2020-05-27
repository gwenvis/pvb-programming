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
			function = (MazeFunctions)Enum.Parse(typeof(MazeFunctions), $"Turn{dropdown.options[dropdown.value].text}");
		}

		public MazeFunctions GetMazeFunction()
		{
			return function;
		}
	}
}
