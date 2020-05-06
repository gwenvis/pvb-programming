using UnityEngine;
using UnityEngine.SceneManagement;
using DN.UI;
using DN.SceneManagement.Data;

namespace DN.SceneManagement
{
    /// <summary>
    /// Here i do the actual loading of a scene through the buttons.
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;

        public void LoadScene(string levelName)
        {
            for (int i = 0; i < levelsData.Levels.Length; i++)
            {
                if (levelsData.Levels[i].SceneName == levelName)
                {
                    SceneManager.LoadScene(levelName, LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log("Error mismatch scenes");
                }
            }
        }
    }
}
