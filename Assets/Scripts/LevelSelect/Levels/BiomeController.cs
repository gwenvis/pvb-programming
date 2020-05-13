using Boo.Lang;
using DN.LevelSelect.SceneManagment;
using DN.Service;
using System.Diagnostics.PerformanceData;
using System.Dynamic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace DN.LevelSelect
{
    /// <summary>
    /// This class look if the biome is cleared and gets the current biome and levels of the biome.
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        public int CurrentBiome => currentBiome;
        public int CurrentLevelsCompleted => currentLevelsCompleted;
        public int SelectedLevelCompleted => selectedLevelCompleted;
        public int CurrentLevelIndex => currentLevelIndex;
        public bool IsBiomeFinished => isBiomeFinished;

        private int currentLevelIndex;
        private int currentBiome;
        private int currentLevelsCompleted;
        private int selectedLevelCompleted;

        private bool isBiomeFinished;

        [SerializeField] private GameObject[] biomes;
        [SerializeField] private GameObject[] levels;

        [SerializeField] private Transform player;

        [SerializeField] private Sprite levelCompletedSprite;

        [SerializeField] private Transform borderParent;
        
        [SerializeField] private BiomeUI biomeUI;

        private GameObject[] borders;

        private GameObject previousBiome;

        private int biomeCount;

        private GameObject prevBiomeObj;
        private GameObject currentBiomeObj;

        private int minBiomeLevelsCompleted = 0;
        private int maxBiomeLevelsCompleted;

        private bool isGameFinished;

        private void Awake()
        {
            SetLevelData();
            SetBiomeStart();
            SetBordersStart();
            GetBiomeLevels();
            CheckForCompletionBiome();
            UpdateBorders();
        }

        private void Update()
        {
            UpdateSelectedLevel();
        }

        private void CheckForCompletionBiome()
        {
            maxBiomeLevelsCompleted = levels.Length;

            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i].GetComponent<LevelData>().isCompleted)
                {
                    currentLevelsCompleted++;
                    levels[i].GetComponentInChildren<SpriteRenderer>().sprite = levelCompletedSprite;
                }
            }

            if (currentLevelsCompleted >= maxBiomeLevelsCompleted)
            {
                currentLevelsCompleted = minBiomeLevelsCompleted;
                previousBiome = currentBiomeObj;
                currentBiome++;


                if (currentBiome != biomeCount)
                {
                    currentBiomeObj = biomes[currentBiome];
                }

                isBiomeFinished = true;
                NextBiome(isBiomeFinished);
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

            if (!isBiomeFinished)
            {
                ServiceLocator.Locate<LevelMemoryService>().SetBiomeFinished(isBiomeFinished);
                for (int i = 0; i < ServiceLocator.Locate<LevelMemoryService>().CompletedLevelIndexes.Count; i++)
                {
                    levels[ServiceLocator.Locate<LevelMemoryService>().CompletedLevelIndexes[i]].GetComponent<LevelData>().isCompleted = true;
                }
            }
            else
            {
                ServiceLocator.Locate<LevelMemoryService>().SetBiomeFinished(!isBiomeFinished);
                ServiceLocator.Locate<LevelMemoryService>().NextBiomeClear(currentBiome);
                ServiceLocator.Locate<LevelMemoryService>().ClearList();
            }
        }

        private void SetBiomeStart()
        {
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

        private void NextBiome(bool value)
        {
            ClearLevels();
            GetBiomeLevels();
            CheckForAllBiomesCleared();
            biomeUI.SetTaskManager(isBiomeFinished, (maxBiomeLevelsCompleted - currentLevelsCompleted));

            currentBiomeObj.SetActive(value);
        }

        private void UpdateBorders()
        {
            for (int i = 0; i < ServiceLocator.Locate<LevelMemoryService>().CompletedBiomeIndexes.Count; i++)
            {
                if(ServiceLocator.Locate<LevelMemoryService>().CompletedBiomeIndexes.Count <= biomeCount)
                {
                    borders[i].SetActive(false);
                }
            }
        }

        private void UpdateSelectedLevel()
        {
            float distance;
            float minDistance = 4f;

            for (int i = 0; i < levels.Length; i++)
            {
                distance = Vector3.Distance(player.position, levels[i].transform.position);
                if(distance <= minDistance)
                {
                    currentLevelIndex = i;
                }
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
            if (currentBiome >= biomeCount)
            {
                isGameFinished = true;
                Debug.Log("Finished Game biem");
            }
        }

        private void SetLevelData()
        {
            currentBiome = ServiceLocator.Locate<LevelMemoryService>().CurrentBiome;
            currentLevelsCompleted = ServiceLocator.Locate<LevelMemoryService>().CurrentLevelsCompleted;
            selectedLevelCompleted = ServiceLocator.Locate<LevelMemoryService>().SelectedLevelCompleted;
            isBiomeFinished = ServiceLocator.Locate<LevelMemoryService>().BiomeFinished;
        }
    }
}
