using UnityEngine;
using UnityEngine.SceneManagement;
using DN.UI;
using DN.SceneManagement.Data;

namespace DN.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]private LevelsData levelsData;

        private HorizontalScroller horizontalScroller;

        private int currentIndex = 0;
        private int btnAmount;

        private void Start()
        {
            horizontalScroller = GetComponent<HorizontalScroller>();

            btnAmount = horizontalScroller.btn.Count + 1;
        }

        private void Update()
        {
            currentIndex = horizontalScroller.currentBtnIndex;
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(levelsData.Levels[currentIndex].SceneName, LoadSceneMode.Single);
        }
    }
}
