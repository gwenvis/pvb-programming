using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DN.UI
{
    /// <summary>
    /// This script is used to check the input of the player.
    /// </summary>
    public static class InputManager
    {
        public static bool HasInput => Input.GetMouseButton(0);

        public static Vector2 CurrentTouchPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);   
    }
}
