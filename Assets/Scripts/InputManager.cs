using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DN.UI
{
    /// <summary>
    /// This script is used to check the input of the player and call functions 
    /// according to those inputs.
    /// </summary>
    public static class InputManager
    {

        public static bool HasInput
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }

        public static Vector2 CurrentTouchPos
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        
    }
}
