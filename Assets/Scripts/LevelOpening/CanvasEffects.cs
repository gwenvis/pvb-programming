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

        [SerializeField] 
        private LevelLoader levelLoader;

        [SerializeField]
        private ParticleSystem sparkParticle;

        private float startZoomPos = 1;
        private float endZoomPos = 8;
        private float duration = 3f;
        private Vector3 desiredPosition;

        private float maxPosX;
        private float minPosX;
        private float maxPosY;
        private float minPosY;

        private bool isClicked = false;

        private void Start()
        {
            maxPosX = animal.rectTransform.anchoredPosition.x + 3f;
            minPosX = animal.rectTransform.anchoredPosition.x - 3f;
            maxPosY = animal.rectTransform.anchoredPosition.y + 3f;
            minPosY = animal.rectTransform.anchoredPosition.y - 3f;

            animal.canvasRenderer.SetAlpha(0.0f);
            triggerField.canvasRenderer.SetAlpha(0.0f);
            blackFade.canvasRenderer.SetAlpha(0.0f);

            desiredPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

            StartCoroutine(FadeInAnimal());
        }

        private IEnumerator FadeInAnimal()
        {
            animal.CrossFadeAlpha(1, 2, false);
            yield return new WaitForSeconds(2f);

            StartCoroutine(Jitter());
            
            triggerField.CrossFadeAlpha(.6f, 2, false);
        }

        public void FadeInBlackScreen()
        {
            blackFade.CrossFadeAlpha(1, 1f, false);
        }

        public void ZoomIn()
        {
            StartCoroutine(startZoom(startZoomPos, endZoomPos));
            isClicked = true;
        }

        public IEnumerator Jitter()
        {
            while(true)
            {
                float timePassed = 0;
                float intervalTime = Random.Range(1, 2);
                yield return new WaitForSeconds(intervalTime);

                if (isClicked)
                {
                    break;
                }

                sparkParticle.Emit(30);

                while (timePassed < 0.3f)
                {
                    float newPosX = Random.Range(minPosX, maxPosX);
                    float newPosY = Random.Range(minPosY, maxPosY);

                    animal.rectTransform.anchoredPosition = new Vector2(newPosX, newPosY);

                    timePassed += Time.deltaTime;
                    yield return null;
                }
            }
        }

        private IEnumerator startZoom(float startZoomPos, float endZoomPos)
        {
            float startTime = Time.time;
            float counter = 0f;

            while (counter < duration)
            {
                float t = (Time.time - startTime) / duration;
                counter += Time.deltaTime * duration;
                animal.transform.localScale = Vector3.one * Mathf.Lerp(startZoomPos, endZoomPos, t);
                animal.transform.position = Vector3.Lerp(animal.transform.position, desiredPosition, t);

                yield return new WaitForEndOfFrame();
            }
            levelLoader.LoadPuzzleScene();
        }
    }
}
