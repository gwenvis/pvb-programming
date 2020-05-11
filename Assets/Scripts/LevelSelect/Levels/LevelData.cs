using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.LevelSelect
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	
	[System.Serializable]
	public partial class LevelData : MonoBehaviour
	{
		public bool isLocked;
		public bool isCompleted;

		public SelectedAnimal AnimalSelected => selectedAnimal;
		public SelectedPuzzle PuzzleSelected => selectedPuzzle;

		[SerializeField] private SelectedAnimal selectedAnimal;
		[SerializeField] private SelectedPuzzle selectedPuzzle;
	}
}
