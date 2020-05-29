using System;
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
        [SerializeField] Animator transition;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip enterLevelSelectClip;
        [SerializeField] private AudioClip exitLevelSelectClip;
        [SerializeField] private AudioClip exitLevelSelectLoseClip;

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
        private const string CRICKET_IBS_NAME = "LevelOpenerCricket";
        private const string DRAGONFLY_IBS_NAME = "LevelOpenerDragonFly";
        private const string SEALION_IBS_NAME = "LevelOpenerSeaLion";
        private const string STICKTAIL_IBS_NAME = "LevelOpenerStickTail";
        private const string WALRUS_IBS_NAME = "LevelOpenerWalrus";
        private const string PENGUIN_IBS_NAME = "LevelOpenerPenguin";
        private const string RACOON_IBS_NAME = "LevelOpenerRacoon";
        private const string SHARK_IBS_NAME = "LevelOpenerShark";

        private string prevSceneLoaded;

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public IEnumerator LoadInBetweenScene()
        {
            ServiceLocator.Locate<SongService>().ExitingLevelSelect();
            
            
            if (levelObject.GetComponent<LevelDataEditor>().PuzzleSelected == selectedPuzzle)
            {
                transition.SetTrigger("Start");

                yield return new WaitForSeconds(2f);
                if (enterLevelSelectClip && audioSource) audioSource.PlayOneShot(enterLevelSelectClip);
                
                switch (selectedAnimal)
                {
                    case LevelDataEditor.SelectedAnimal.Dragonfly:
                        GetAndSetScene(DRAGONFLY_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Cricket:
                        GetAndSetScene(CRICKET_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Sealion:
                        GetAndSetScene(SEALION_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Sticktail:
                        GetAndSetScene(STICKTAIL_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Walrus:
                        GetAndSetScene(WALRUS_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Bug:
                        GetAndSetScene(BUG_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Hog:
                        GetAndSetScene(HOG_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Penguin:
                        GetAndSetScene(PENGUIN_IBS_NAME);
                        break;

                    case LevelDataEditor.SelectedAnimal.Racoon:
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

            if(this)
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

        public void StartIntroVideo()
        {
            StartCoroutine(IntroVideo());
        }

        public IEnumerator IntroVideo()
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(2f);

            //SceneManager.LoadScene("IntroVideo", LoadSceneMode.Additive);
            GetAndSetScene("IntroVideo");
        }

        public IEnumerator StartEndAnimation()
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(2f);

            GetAndSetScene("EndVideo");
        }

        public IEnumerator StartEndTransition()
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(2f);

            GetAndSetScene("MainMenu");
        }

        public IEnumerator LoadLevelSelect()
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(2f);

            GetAndSetScene("LevelSelect");
        }

        public IEnumerator LoadLevelSelectFromPuzzle(bool isGameWon)
        {
            yield return new WaitForSeconds(2f);

            transition.SetTrigger("End");

            GetAndSetScene(LEVEL_SELECT_NAME);
            levelObject.GetComponent<LevelDataEditor>().isCompleted = isGameWon;
            ServiceLocator.Locate<LevelMemoryService>().BiomeController.CompletedLevel();
            ServiceLocator.Locate<SongService>().EnteringLevelSelect();

            AudioClip clip = isGameWon ? exitLevelSelectClip : exitLevelSelectLoseClip;
            if (clip && audioSource) audioSource.PlayOneShot(clip);
        }
    }
}
