using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.SceneManagement.Data
{
    public class Level
    {
        private string LevelName => levelName;
        private string SevelName => sceneName;

        [SerializeField]private string levelName;
        [SerializeField]private string sceneName;
    }
}

