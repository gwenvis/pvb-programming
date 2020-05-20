using DN.UI;
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

        [SerializeField] private Transform player;

        [SerializeField] private Sprite levelCompletedSprite;

        [SerializeField] private Transform borderParent;
        
        [SerializeField] private BiomeUI biomeUI;

        private GameObject[] borders;

        private int previousBiome;
        private int biomeCount;
        private int currentBiome;

        private int currentLevelsCompleted;

        private bool isBiomeFinished;

        private GameObject currentBiomeObj;

        private int minBiomeLevelsCompleted = 0;
        private int maxBiomeLevelsCompleted;

        private void Awake()
        {
            SetBiomeStart();
            SetBordersStart();
            GetBiomeLevels();
            CompletedLevel();
        }

        public void CompletedLevel()
        {
            CheckForCompletionBiome();
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
                currentBiome++;

                if (currentBiome != biomeCount)
                {
                    currentBiomeObj = biomes[currentBiome];
                }

                isBiomeFinished = true;
                previousBiome = currentBiome - 1;
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
            UpdateBorders();
            biomeUI.SetTaskManager(isBiomeFinished, (maxBiomeLevelsCompleted - currentLevelsCompleted));

            currentBiomeObj.SetActive(value);
        }

        private void UpdateBorders()
        {
            borders[previousBiome].SetActive(false);
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
                Debug.Log("Finished Game biem");
            }
        }
    }
}
