using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.SceneManagement.Data
{
    public class LevelsData : ScriptableObject
    {
        public Level[] Levels => levels;

        [SerializeField]private Level[] levels;
    }
}