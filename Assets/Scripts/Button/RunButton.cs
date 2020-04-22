using System;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// Uses event to start puzzle.
	/// </summary>
	public class RunButton : MonoBehaviour
	{
		public event Action RunPuzzleEvent;

		public void RunPuzzle()
		{
			RunPuzzleEvent?.Invoke();
		}
	}
}
