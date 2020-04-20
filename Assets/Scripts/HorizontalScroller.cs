using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class HorizontalScroller : MonoBehaviour
{
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private Button[] btn;
    [SerializeField]
    private RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int btnDistance;
    private int minBtnNumb;

    private int btnAmount = 6;
    private List<string> levelNames = new List<string>();

    private int index = 0;

    private void Start()
    {
        int btnLength = btn.Length;
        distance = new float[btnLength];

        btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x - btn[0].GetComponent<RectTransform>().anchoredPosition.x);
        //print(btnDistance + (60 * btnLength));

        for (int i = 1; i < btnAmount; i++)
        {
            Object path = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Levels/Level" + i + ".unity", typeof(SceneAsset));
            levelNames.Add(path.name);
        }
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
                index = j;
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

    public void LoadScene()
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene(levelNames[0], LoadSceneMode.Single);
                break;

            case 1:
                SceneManager.LoadScene(levelNames[1], LoadSceneMode.Single);
                break;

            case 2:
                SceneManager.LoadScene(levelNames[2], LoadSceneMode.Single);
                break;

            case 3:
                SceneManager.LoadScene(levelNames[3], LoadSceneMode.Single);
                break;

            case 4:
                SceneManager.LoadScene(levelNames[4], LoadSceneMode.Single);
                break;
        }
    }
}
