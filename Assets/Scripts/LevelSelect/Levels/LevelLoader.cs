using DN.Service;
using System.Collections;
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

        private const string LEVEL_SELECT_NAME = "LevelSelect";
        private const string DOG_IBS_NAME = "LevelOpenerDragonfly";
        private const string OWL_IBS_NAME = "LevelOpenerOwl";

        private string prevSceneLoaded;

        public void LoadInBetweenScene()
        {
            if (levelObject.GetComponent<LevelDataEditor>().PuzzleSelected == selectedPuzzle)
            {
                ServiceLocator.Locate<LevelMemoryService>().SetAudioListener.SetListener(false);

                switch (selectedAnimal)
                {
                    case LevelDataEditor.SelectedAnimal.Dog:
                        GetAndSetScene(DOG_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Owl:
                        GetAndSetScene(OWL_IBS_NAME);
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
            GetAndSetScene(selectedPuzzle.ToString());
        }

        public void SetLoadingLevelData(GameObject other, LevelDataEditor.SelectedPuzzle puzzle, LevelDataEditor.SelectedAnimal animal)
        {
            levelObject = other;
            selectedPuzzle = puzzle;
            selectedAnimal = animal;
        }

        public void LoadLevelSelect()
        {
            SceneManager.LoadScene(LEVEL_SELECT_NAME);
        }

        public void LoadLevelSelectFromPuzzle(bool isGameWon)
        {
            GetAndSetScene(LEVEL_SELECT_NAME);
            levelObject.GetComponent<LevelDataEditor>().isCompleted = isGameWon;
            ServiceLocator.Locate<LevelMemoryService>().BiomeController.CompletedLevel();
            ServiceLocator.Locate<LevelMemoryService>().SetAudioListener.SetListener(true);
        }
    }
}
