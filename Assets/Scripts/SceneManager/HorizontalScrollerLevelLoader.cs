using UnityEngine;
using UnityEngine.SceneManagement;
using DN.UI;
using DN.SceneManagement.Data;

namespace DN.SceneManagement
{
    /// <summary>
    /// Here i do the actual loading of a scene through the buttons.
    /// </summary>
    public class HorizontalScrollerLevelLoader : MonoBehaviour
    {

        [SerializeField] private LevelsData levelsData;

        public void LoadScene(int currentIndex)
        {
            SceneManager.LoadScene(levelsData.Levels[currentIndex].SceneName, LoadSceneMode.Single);
        }
    }
}
