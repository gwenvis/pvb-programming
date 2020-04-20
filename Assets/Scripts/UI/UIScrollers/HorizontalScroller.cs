using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace DN.UI
{
    public class HorizontalScroller : MonoBehaviour
    {
        public Button[] btn;
        public int currentBtnIndex = 0;

        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform center;

        private float[] distance;
        private bool dragging = false;

        private int btnDistance;
        private int minBtnNumb;

        private void Start()
        {
            int btnLength = btn.Length;
            distance = new float[btnLength];

            btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x - btn[0].GetComponent<RectTransform>().anchoredPosition.x);
        }

        private void Update()
        {
            for (int i = 0; i < btn.Length; i++)
            {
                distance[i] = Mathf.Abs(center.transform.position.x - btn[i].transform.position.x);
            }

            float minDistance = Mathf.Min(distance);

            for (int j = 0; j < btn.Length; j++)
            {
                if (minDistance == distance[j])
                {
                    minBtnNumb = j;
                    currentBtnIndex = j;
                }
            }

            if (!dragging)
            {
                LerpToBtn(minBtnNumb * -btnDistance);
            }
        }

        private void LerpToBtn(int position)
        {
            float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
            Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

            panel.anchoredPosition = newPosition;
        }

        public void StartDrag()
        {
            dragging = true;
        }

        public void EndDrag()
        {
            dragging = false;
        }
    }
}

