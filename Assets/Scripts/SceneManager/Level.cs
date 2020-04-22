using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public Sprite LevelImage => levelImage;

        [SerializeField] private string levelName;
        [SerializeField] private string sceneName;
        [SerializeField] private bool isLocked;
        [SerializeField] private Sprite levelImage;
    }
}

