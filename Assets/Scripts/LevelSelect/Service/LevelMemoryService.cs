using DN.Service;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	[Service]
	public class LevelMemoryService
	{
		public Vector3 PlayerPos => playerPos;
		public Quaternion PlayerRot => playerRot;
		public int CurrentBiome => currentBiome;
		public int CurrentLevelsCompleted => currentLevelsCompleted;
		public int SelectedLevelCompleted => selectedLevelCompleted;
		public LevelData.SelectedPuzzle SelectedPuzzle => selectedPuzzle;
		public LevelData.SelectedAnimal SelectedAnimal => selectedAnimal;
		public bool PlayerPosSetOnce => playerPosSetOnce;
		public bool BiomeSetOnce => biomeSetOnce;
		public bool BiomeFinished => biomeFinished;
		public bool IsGameWon => isGameWon;
		public List<int> CompletedLevelIndexes => completedLevelIndexes;
		public List<int> CompletedBiomeIndexes => completedBiomeIndexes;

		private List<int> completedLevelIndexes = new List<int>();
		private List<int> completedBiomeIndexes = new List<int>();

		private LevelData.SelectedPuzzle selectedPuzzle;
		private LevelData.SelectedAnimal selectedAnimal;

		private Vector3 playerPos;
		private Quaternion playerRot;

		private bool playerPosSetOnce;
		private bool biomeSetOnce;
		private bool biomeFinished;
		private bool isGameWon;

		private int currentBiome;
		private int currentLevelsCompleted;
		private int selectedLevelCompleted;

		public void SetData(int currBiome, int currLevelCompleted, LevelData.SelectedPuzzle puzzle, LevelData.SelectedAnimal animal, int selectedLevel, int currenLevelInd)
		{
			completedLevelIndexes.Add(currenLevelInd);
			selectedLevelCompleted = selectedLevel;
			currentBiome = currBiome;
			currentLevelsCompleted = currLevelCompleted;
			selectedPuzzle = puzzle;
			selectedAnimal = animal;
		}

		public void SetVehicleData(Vector3 playerPo, Quaternion playerRo, bool setOnce)
		{
			playerRot = playerRo;
			playerPos = playerPo;
			playerPosSetOnce = setOnce;
		}

		public void NextBiomeClear(int currBiome)
		{
			completedBiomeIndexes.Add(currBiome);
		}
		public void SetBiomeFinished(bool value)
		{
			biomeFinished = value;
		}
		public void ClearList()
		{
			completedLevelIndexes.Clear();
		}

		public void SetGameWonOrLost(bool value)
		{
			isGameWon = value;
		}
	}
}
