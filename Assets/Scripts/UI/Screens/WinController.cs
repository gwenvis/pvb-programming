using System;
using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// Controlls if the game is won
	/// </summary>
	public class WinController : MonoBehaviour
	{
		public event Action GameWonEvent;

		public void WonGame()
		{
			GameWonEvent?.Invoke();
		}
	}
}
