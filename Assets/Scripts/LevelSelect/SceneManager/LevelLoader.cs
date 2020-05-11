using UnityEngine;
using UnityEngine.SceneManagement;

namespace DN.LevelSelect.SceneManagment
{
    /// <summary>
    /// This is the actual scene loader where you load thge selected scene
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelData levelsData;

        private const string LEVEL_SELECT_NAME = "LevelSelect";

        public void LoadScene(GameObject other, LevelData.SelectedPuzzle levelName, bool isLocked)
        {
            if (other.GetComponent<LevelData>().PuzzleSelected == levelName && !isLocked)
            {
                SceneManager.LoadScene(levelName.ToString(), LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Error mismatch scenes");
            }
        }

        public void LoadLevelSelect()
        {
            SceneManager.LoadScene(LEVEL_SELECT_NAME);
        }
    }
}
