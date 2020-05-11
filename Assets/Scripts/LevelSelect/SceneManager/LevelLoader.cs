using UnityEngine;
using UnityEngine.SceneManagement;
using DN.SceneManagement.Data;

namespace DN.Levelselect.SceneManagment
{
    /// <summary>
    /// This is the actual scene loader where you load thge selected scene
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;

        private const string LEVEL_SELECT_NAME = "LevelSelect";

        public void LoadScene(string levelName)
        {
            for (int i = 0; i < levelsData.Levels.Length; i++)
            {
                if (levelsData.Levels[i].LevelName == levelName && !levelsData.Levels[i].IsLocked)
                {
                    SceneManager.LoadScene(levelName, LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log("Error mismatch scenes");
                }
            }
        }

        public void LoadLevelSelect()
        {
            SceneManager.LoadScene(LEVEL_SELECT_NAME);
        }
    }
}
