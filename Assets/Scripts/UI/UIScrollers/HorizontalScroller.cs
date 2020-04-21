using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DN.SceneManagement.Data;

namespace DN.UI
{
    public class HorizontalScroller : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;

        public List<Button> btn = new List<Button>();
        public int currentBtnIndex = 0;

        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform center;

        [SerializeField] private GameObject parent;

        private float[] distance;
        private bool dragging = false;

        private int btnDistance;
        private int minBtnNumb;

        private Vector2 minSize = new Vector2(350, 250);
        private Vector2 maxSize = new Vector2(450, 350);

        private const int ORIGIN_DISTANCE = 510;
        private const float LERP_SPEED = 10f;

        private List<RectTransform> currentRect = new List<RectTransform>();
        private List<Button> currentButton = new List<Button>();

        private void Start()
        {
            int btnLength = btn.Count;
            distance = new float[btnLength];

            for (int i = 0; i < btn.Count; i++)
            {
                currentRect.Add(btn[i].transform.GetChild(0).GetComponent<RectTransform>());
                currentButton.Add(btn[i].transform.GetChild(0).GetComponent<Button>());
            }
        }

        private void Update()
        {
            if (btnDistance != ORIGIN_DISTANCE)
            {
                CalculateDistance();
            }

            for (int i = 0; i < btn.Count; i++)
            {
                distance[i] = Mathf.Abs(center.transform.position.x - btn[i].transform.position.x);
            }

            float minDistance = Mathf.Min(distance);

            for (int j = 0; j < btn.Count; j++)
            {
                if (minDistance == distance[j])
                {
                    minBtnNumb = j;
                    currentBtnIndex = j;
                    LerpBetweenButtonsSize(j, maxSize);
                    SetButtonInteractive(j, true);
                }
                else
                {
                    LerpBetweenButtonsSize(j, minSize);
                    SetButtonInteractive(j, false);
                }
            }

            if (!dragging)
            {
                LerpToBtn(minBtnNumb * -btnDistance);
            }
        }

        private void LerpToBtn(int position)
        {
            float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * LERP_SPEED);
            Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

            panel.anchoredPosition = newPosition;
        }

        private void SetButtonInteractive(int i, bool value)
        {
            currentButton[i].interactable = value;
        }

        private void LerpBetweenButtonsSize(int i, Vector2 size)
        {
            currentRect[i].sizeDelta = Vector2.Lerp(currentRect[i].sizeDelta, size, Time.deltaTime * LERP_SPEED);
        }

        public void StartDrag()
        {
            dragging = true;
        }

        public void EndDrag()
        {
            dragging = false;
        }

        public void CalculateDistance()
        {
            btnDistance = (int)Mathf.Abs(btn[0].GetComponent<RectTransform>().anchoredPosition.x - btn[1].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }
}

