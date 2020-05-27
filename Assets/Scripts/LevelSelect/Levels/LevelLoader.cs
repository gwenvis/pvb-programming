using DN.Service;
using System.Collections;
using DN.Music;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DN.LevelSelect.SceneManagment
{
    /// <summary>
    /// This is the actual scene loader where you load thge selected scene
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        public GameObject LevelObject => levelObject;
        public LevelDataEditor.SelectedPuzzle SelectedPuzzle => selectedPuzzle;
        public LevelDataEditor.SelectedAnimal SelectedAnimal => selectedAnimal;

        private GameObject levelObject;
        private LevelDataEditor.SelectedPuzzle selectedPuzzle;
        private LevelDataEditor.SelectedAnimal selectedAnimal;
        private DN.LevelData levelData;

        private const string LEVEL_SELECT_NAME = "LevelSelect";
        private const string BUG_IBS_NAME = "LevelOpenerBug";
        private const string HOG_IBS_NAME = "LevelOpenerHog";
        private const string PENGUIN_IBS_NAME = "LevelOpenerPenguin";
        private const string RACOON_IBS_NAME = "LevelOpenerRacoon";
        private const string SHARK_IBS_NAME = "LevelOpenerShark";

        private string prevSceneLoaded;

        public void LoadInBetweenScene()
        {
            ServiceLocator.Locate<SongService>().ExitingLevelSelect();
            
            if (levelObject.GetComponent<LevelDataEditor>().PuzzleSelected == selectedPuzzle)
            {
                switch (selectedAnimal)
                {
                    case LevelDataEditor.SelectedAnimal.Bug:
                        GetAndSetScene(BUG_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Hog:
                        GetAndSetScene(HOG_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Racoon:
                        GetAndSetScene(PENGUIN_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Penguin:
                        GetAndSetScene(RACOON_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Shark:
                        GetAndSetScene(SHARK_IBS_NAME);
                        break;
                }
            }
        }

        public void GetAndSetScene(string sceneName)
        {
            prevSceneLoaded = SceneManager.GetActiveScene().name;

            Scene loadedLevel = SceneManager.GetSceneByName(sceneName);
            if (loadedLevel.isLoaded)
            {
                SceneManager.SetActiveScene(loadedLevel);
                SceneManager.UnloadScene(prevSceneLoaded);
                return;
            }

            StartCoroutine(LoadLevel(sceneName, prevSceneLoaded));
        }

        IEnumerator LoadLevel(string sceneName, string prevSceneName)
        {
            enabled = false;
            yield return SceneManager.LoadSceneAsync(
                sceneName, LoadSceneMode.Additive
            );

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            if (prevSceneName != LEVEL_SELECT_NAME)
            {
                SceneManager.UnloadScene(prevSceneName);
            }

            enabled = true;
        }

        public void LoadPuzzleScene()
        {
            ServiceLocator.Locate<LevelMemoryService>().LevelData = levelData;
            GetAndSetScene(selectedPuzzle.ToString());
        }

        public void SetLoadingLevelData(GameObject other, LevelDataEditor.SelectedPuzzle puzzle, LevelDataEditor.SelectedAnimal animal, LevelData level)
        {
            levelObject = other;
            selectedPuzzle = puzzle;
            selectedAnimal = animal;
            levelData = level;
        }

        public void LoadLevelSelect()
        {
            void SetScene(AsyncOperation operation)
            {
                operation.allowSceneActivation = true;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(LEVEL_SELECT_NAME));
                Debug.Log("loading complete");
            }

            var loadSceneAsync = SceneManager.LoadSceneAsync(LEVEL_SELECT_NAME, LoadSceneMode.Additive);
            loadSceneAsync.completed += SetScene;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void LoadLevelSelectFromPuzzle(bool isGameWon)
        {
            GetAndSetScene(LEVEL_SELECT_NAME);
            levelObject.GetComponent<LevelDataEditor>().isCompleted = isGameWon;
            ServiceLocator.Locate<LevelMemoryService>().BiomeController.CompletedLevel();
            ServiceLocator.Locate<SongService>().EnteringLevelSelect();
        }
    }
}
