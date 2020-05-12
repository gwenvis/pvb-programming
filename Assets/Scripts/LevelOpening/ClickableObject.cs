using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI.LevelOpener.Object
{
    /// <summary>
    ///This scripts tracks if the object is clicked and handles everything after that.
    /// </summary>

    public class ClickableObject : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private CanvasEffects canvasEffects;

        public void OnPointerDown(PointerEventData eventData)
        {
            canvasEffects.FadeInBlackScreen();
            canvasEffects.ZoomIn();
        }
    }
}
