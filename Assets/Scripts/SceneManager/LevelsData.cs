using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.SceneManagement.Data
{
    /// <summary>
    /// This is where i store the LevelData
    /// </summary>
    public class LevelsData : ScriptableObject
    {
        public Level[] Levels => levels;

        [SerializeField]private Level[] levels;
    }
}