using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.SceneManagement.Data
{
    /// <summary>
    /// This is what my data is set.
    /// </summary>
    [System.Serializable]
    public class Level
    {
        public string LevelName => levelName;
        public string SceneName => sceneName;
        public bool IsLocked => isLocked;

        [SerializeField] private string levelName;
        [SerializeField] private string sceneName;
        [SerializeField] private bool isLocked;
    }
}

