using DN.LevelSelect.Player;
using DN.Service;
using System.ComponentModel;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DN.LevelSelect.SceneManagment
{
    /// <summary>
    /// This is the actual scene loader where you load thge selected scene
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [HideInInspector] public bool isInBetweenFinished;

        public GameObject LevelObject => levelObject;
        public LevelData.SelectedPuzzle SelectedPuzzle => selectedPuzzle;
        public LevelData.SelectedAnimal SelectedAnimal => selectedAnimal;

        private GameObject levelObject;
        private LevelData.SelectedPuzzle selectedPuzzle;
        private LevelData.SelectedAnimal selectedAnimal;

        private const string LEVEL_SELECT_NAME = "LevelSelect";
        private const string DOG_IBS_NAME = "DogIBS";
        private const string OWL_IBS_NAME = "OwlIBS";

        public void GetData()
        {
            selectedPuzzle = ServiceLocator.Locate<LevelMemoryService>().SelectedPuzzle;
            selectedAnimal = ServiceLocator.Locate<LevelMemoryService>().SelectedAnimal;
        }

        public void LoadInBetweenScene()
        {
            GetData();
            if (levelObject.GetComponent<LevelData>().PuzzleSelected == selectedPuzzle)
            {
                switch (selectedAnimal)
                {
                    case LevelData.SelectedAnimal.Dog:
                        SceneManager.LoadScene(DOG_IBS_NAME, LoadSceneMode.Single);
                        break;

                    case LevelData.SelectedAnimal.Owl:
                        SceneManager.LoadScene(OWL_IBS_NAME, LoadSceneMode.Single);
                        break;
                }
            }
        }

        public void LoadPuzzleScene()
        {
            GetData();
            SceneManager.LoadScene(selectedPuzzle.ToString(), LoadSceneMode.Single);
            isInBetweenFinished = false;
        }

        public void SetLoadingLevelData(GameObject other, LevelData.SelectedPuzzle puzzle, LevelData.SelectedAnimal animal)
        {
            levelObject = other;
            selectedPuzzle = puzzle;
            selectedAnimal = animal;
        }

        public void LoadLevelSelect()
        {
            GetData();
            SceneManager.LoadScene(LEVEL_SELECT_NAME);
        }
    }
}
