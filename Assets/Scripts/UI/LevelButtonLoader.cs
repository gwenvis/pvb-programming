using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DN.SceneManagement.Data;
using DN.SceneManagement;

namespace DN.UI
{
    public class LevelButtonLoader : MonoBehaviour
    {
        [SerializeField]private LevelsData levelsData;
        [SerializeField] private HorizontalScroller horizontalScroller;

        [SerializeField]private Button buttonPrefab;
        [SerializeField]private GameObject parent;

        private LevelLoader levelLoader;

        private void Start()
        {
            levelLoader = GetComponent<LevelLoader>();
            horizontalScroller = GetComponent<HorizontalScroller>();

            InitializeButtonSpawn();
            Debug.Log("man");
        }

        private void InitializeButtonSpawn()
        {
            for (int i = 0; i < levelsData.Levels.Length; i++)
            {
                Button button = (Button)Instantiate(buttonPrefab, parent.transform);
                //button.GetComponent<Button>().onClick.AddListener(levelLoader.LoadScene);
                button.gameObject.name = "Level" + (i + 1) + "Button";
                horizontalScroller.btn.Add(button);
                //button.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            }
        }
    }
}

