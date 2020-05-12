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

        [SerializeField] private Transform borderParent;
        private GameObject[] borders;

        [SerializeField] private BiomeUI biomeUI;

        private GameObject currentBiomeObj;
        private GameObject previousBiome;

        private int currentBiome;
        private int biomeCount;

        private int currentLevelsCompleted;
        private int minBiomeLevelsCompleted = 0;
        private int maxBiomeLevelsCompleted;

        private bool isBiomeFinished;
        private bool isGameFinished;

        private void Awake()
        {
            SetBiomeStart();
            SetBordersStart();
            GetBiomeLevels();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                CheckForCompletionBiome();
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
                    levels[i].SetActive(false);
                }
            }

            if (currentLevelsCompleted == maxBiomeLevelsCompleted)
            {
                previousBiome = currentBiomeObj;
                currentLevelsCompleted = minBiomeLevelsCompleted;
                currentBiome++;

                if(currentBiome != biomeCount)
                {
                    currentBiomeObj = biomes[currentBiome];
                }

                isBiomeFinished = true;
                NextBiome(isBiomeFinished, currentBiome);
            }
            else
            {
                biomeUI.SetTaskManager(isBiomeFinished, (maxBiomeLevelsCompleted - currentLevelsCompleted));
                currentLevelsCompleted = minBiomeLevelsCompleted;
                isBiomeFinished = false;
            }
        }
        private void SetBordersStart()
        {
            borders = new GameObject[borderParent.childCount];
            for (int i = 0; i < borderParent.childCount; i++)
            {
                borders[i] = borderParent.GetChild(i).gameObject;
            }
        }

        private void GetBiomeLevels()
        {
            levels = new GameObject[currentBiomeObj.transform.childCount];
            for (int i = 0; i < currentBiomeObj.transform.childCount; i++)
            {
                levels[i] = currentBiomeObj.transform.GetChild(i).gameObject;
            }
        }

        private void SetBiomeStart()
        {
            currentBiome = 0;
            biomes = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                biomes[i] = transform.GetChild(i).gameObject;
                biomes[i].SetActive(false);
                biomeCount++;
            }
            currentBiomeObj = biomes[currentBiome];
            currentBiomeObj.SetActive(true);
        }

        private void NextBiome(bool value, int currentCompletedBiome)
        {
            ClearLevels();
            GetBiomeLevels();
            CheckForAllBiomesCleared();
            biomeUI.SetTaskManager(isBiomeFinished, (maxBiomeLevelsCompleted - currentLevelsCompleted));

            currentBiomeObj.SetActive(value);
            previousBiome.SetActive(!value);

            UpdateBorders(currentCompletedBiome);
        }

        private void UpdateBorders(int currentBiomeCleared)
        {
            borders[currentBiomeCleared - 1].SetActive(false);
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
            if (currentBiome >= biomeCount)
            {
                isGameFinished = true;
                Debug.Log("Finished Game biem");
            }
        }
    }
}
