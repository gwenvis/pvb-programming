using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using DN.UI;

namespace DN.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private HorizontalScroller horizontalScroller;

        private int currentIndex = 0;
        private int btnAmount;

        private List<string> levelNames = new List<string>();

        private void Start()
        {
            horizontalScroller = GetComponent<HorizontalScroller>();
                
            btnAmount = horizontalScroller.btn.Length + 1;

            for (int i = 1; i < btnAmount; i++)
            {
                Object path = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level" + i + ".unity", typeof(SceneAsset));
                levelNames.Add(path.name);
            }
        }

        private void Update()
        {
            currentIndex = horizontalScroller.currentBtnIndex;
        }

        public void LoadScene()
        {
            switch (currentIndex)
            {
                case 0:
                    SceneManager.LoadScene(levelNames[0], LoadSceneMode.Single);
                    break;

                case 1:
                    SceneManager.LoadScene(levelNames[1], LoadSceneMode.Single);
                    break;

                case 2:
                    SceneManager.LoadScene(levelNames[2], LoadSceneMode.Single);
                    break;

                case 3:
                    SceneManager.LoadScene(levelNames[3], LoadSceneMode.Single);
                    break;

                case 4:
                    SceneManager.LoadScene(levelNames[4], LoadSceneMode.Single);
                    break;
            }
        }
    }
}
