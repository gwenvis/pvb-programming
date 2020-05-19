using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.LevelSelect
{
	/// <summary>
	/// This is where the data gets stored per level object in the scene.
	/// </summary>
	
	[System.Serializable]
	public partial class LevelData : MonoBehaviour
	{
		public bool isCompleted;

		public SelectedAnimal AnimalSelected => selectedAnimal;
		public SelectedPuzzle PuzzleSelected => selectedPuzzle;

		[SerializeField] private SelectedAnimal selectedAnimal;
		[SerializeField] private SelectedPuzzle selectedPuzzle;
	}
}
