using DN.LevelSelect.SceneManagment;
using DN.UI.LevelOpener.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
    /// <summary>
    /// In this script are the effects that are being used for the Level opening.
    /// </summary>
    public class CanvasEffects : MonoBehaviour
    {
        [SerializeField]
        private Image animal;

        [SerializeField]
        private Image blackFade;

        [SerializeField]
        private Image triggerField;

        [SerializeField] private LevelLoader levelLoader;

        private float startZoomPos;
        private float endZoomPos;
        private float duration = 2;
        private Vector3 desiredPosition;

        void Start()
        {
            animal.canvasRenderer.SetAlpha(0.0f);
            triggerField.canvasRenderer.SetAlpha(0.0f);
            blackFade.canvasRenderer.SetAlpha(0.0f);

            desiredPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

            StartCoroutine(FadeInAnimal());
        }

        private IEnumerator FadeInAnimal()
        {
            yield return new WaitForSeconds(2f);

            animal.CrossFadeAlpha(1, 2, false);
            triggerField.CrossFadeAlpha(.6f, 2, false);
        }

        public void FadeInBlackScreen()
        {
            blackFade.CrossFadeAlpha(1, 2, false);
        }

        public void ZoomIn()
        {
            startZoomPos = 1;
            endZoomPos = 10;

            StartCoroutine(startZoom(startZoomPos, endZoomPos));
        }

        IEnumerator startZoom(float startZoomPos, float endZoomPos)
        {
            float startTime = Time.time;
            float counter = 0f;

            while (counter < duration)
            {
                float t = (Time.time - startTime) / duration;
                counter += Time.deltaTime / duration;
                animal.transform.localScale = Vector3.one * Mathf.Lerp(startZoomPos, endZoomPos, t);
                animal.transform.position = Vector3.Lerp(animal.transform.position, desiredPosition, t);

                yield return new WaitForEndOfFrame();

                //a;ofbnpei;oagnpaoifghb NIET COPYRIGHT MY CODE DOING MY NO COPY    
            }
            levelLoader.LoadPuzzleScene();
        }
    }
}
