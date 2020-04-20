using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.SceneManagement.Data
{
    [System.Serializable]
    public class Level
    {
        public string LevelName => levelName;
        public string SceneName => sceneName;

        [SerializeField]private string levelName;
        [SerializeField]private string sceneName;
    }
}

