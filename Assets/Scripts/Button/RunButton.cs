using System;
using UnityEngine;
using UnityEngine.UI;

namespace DN
{
	/// <summary>
	/// Uses event to start puzzle. Add this script to the run button Or use the run Button Prefab.
	/// </summary>
	public class RunButton : MonoBehaviour
	{
		public event Action RunPuzzleEvent;

		private Button runButton;

		private void Start()
		{
			runButton = GetComponent<Button>();
			runButton.onClick.AddListener(Run);
		}

		public void Run()
		{
			RunPuzzleEvent?.Invoke();
		}

		private void OnDestroy()
		{
			runButton.onClick.RemoveListener(Run);
		}
	}
}
