using DN.UI.LevelOpener.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
    /// <summary>

    /// </summary>
    public class CanvasEffects : MonoBehaviour
    {
        [SerializeField]
        private Image fadingDogImage;

        [SerializeField]
        private Image blackFade;

        [SerializeField]
        private Image triggerField;

        private Canvas canvas;
        private float duration = 2;

        void Start()
        {
            fadingDogImage.canvasRenderer.SetAlpha(0.0f);
            triggerField.canvasRenderer.SetAlpha(0.0f);
            blackFade.canvasRenderer.SetAlpha(0.0f);

            canvas = GetComponent<Canvas>();

            StartCoroutine(FadeInDog());
        }

        private IEnumerator FadeInDog()
        {
            yield return new WaitForSeconds(2f);

            fadingDogImage.CrossFadeAlpha(1, 2, false);
            triggerField.CrossFadeAlpha(.6f, 2, false);
        }

        public void FadeInBlackScreen()
        {
            blackFade.CrossFadeAlpha(1, 2, false);
        }

        public void ZoomIn()
        {
            StartCoroutine(startLerp(canvas.scaleFactor, canvas.scaleFactor*10));
        }

        IEnumerator startLerp(float start, float end)
        {
            float counter = 0f;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                canvas.scaleFactor = Mathf.Lerp(start, end, counter / duration);

                yield return null;
            }
        }
    }
}
