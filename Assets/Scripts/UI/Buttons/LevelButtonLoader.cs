using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DN.SceneManagement.Data;
using DN.SceneManagement;

namespace DN.UI
{
    /// <summary>
    /// In this script i spawn the buttons and give them their name and listener so the buttons can act.
    /// </summary>
    public class LevelButtonLoader : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;
        [SerializeField] private HorizontalScroller horizontalScroller;

        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject parent;

        private HorizontalScrollerLevelLoader levelLoader;

        private void Awake()
        {
            levelLoader = GetComponent<HorizontalScrollerLevelLoader>();
            horizontalScroller = GetComponent<HorizontalScroller>();

            InitializeButtonSpawn();
        }

        private void InitializeButtonSpawn()
        {
            for (int i = 0; i < levelsData.Levels.Length; i++)
            {
                Button button = (Button)Instantiate(buttonPrefab, parent.transform);
                button.gameObject.name = levelsData.Levels[i].LevelName;
                button.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnClickEvent);
                button.transform.GetChild(0).GetComponentInChildren<Text>().text = levelsData.Levels[i].LevelName;
                horizontalScroller.btn.Add(button);
            }
        }

        private void OnClickEvent()
        {
            levelLoader.LoadScene(horizontalScroller.currentBtnIndex);
        }
    }
}

