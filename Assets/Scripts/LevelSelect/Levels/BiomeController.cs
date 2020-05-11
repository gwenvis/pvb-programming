using System.Dynamic;
using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// This class look if the biome is cleared and gets the current biome and levels of the biome.
	/// </summary>
	public class BiomeController : MonoBehaviour
	{
		[SerializeField] private GameObject[] biomes;
		[SerializeField] private GameObject[] levels;

		private GameObject currentBiomeObj;

		private int currentBiome;
		private int biomeCount;

		private int currentLevelsCompleted ;
		private int minBiomeLevelsCompleted = 0;
		private int maxBiomeLevelsCompleted;

		private void Awake()
		{
			SetBiomeStart();
			GetBiomeLevels();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				CheckForCompletionBiome();
			}
		}

		private void SetBiomeStart()
		{
			currentBiome = 0;
			biomes = new GameObject[transform.childCount];
			for (int i = 0; i < transform.childCount; i++)
			{
				biomes[i] = transform.GetChild(i).gameObject;
				biomeCount++;
			}
			currentBiomeObj = biomes[currentBiome];
		}

		private void GetBiomeLevels()
		{
			levels = new GameObject[currentBiomeObj.transform.childCount];
			for (int i = 0; i < currentBiomeObj.transform.childCount; i++)
			{
				levels[i] = currentBiomeObj.transform.GetChild(i).gameObject;
			}
		}

		private void CheckForCompletionBiome()
		{
			maxBiomeLevelsCompleted = levels.Length;
			for (int i = 0; i < levels.Length; i++)
			{
				if (levels[i].GetComponent<LevelData>().isCompleted)
				{
					currentLevelsCompleted++;
				}
				else
				{
					currentLevelsCompleted = minBiomeLevelsCompleted;
					break;
				}
			}
			if(currentLevelsCompleted == maxBiomeLevelsCompleted)
			{
				currentLevelsCompleted = minBiomeLevelsCompleted;
				currentBiome++;
				currentBiomeObj = biomes[currentBiome];
				ClearLevels();
				GetBiomeLevels();
			}
		}

		private void ClearLevels()
		{
			for (int i = 0; i < levels.Length; i++)
			{
				levels[i] = null;
			}
		}

		private void CheckForAllBiomesCleared()
		{
			if(currentBiome >= biomeCount)
			{
				Debug.Log("Finished Game biem");
			}
		}
	}
}
