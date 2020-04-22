using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodeCollector : MonoBehaviour
{

    [SerializeField] private GameObject container;
    [SerializeField] private GameObject codeButtonPrefab;

    [SerializeField] private RectTransform code1ScrollingPanel;
    [SerializeField] private RectTransform code2ScrollingPanel;
    [SerializeField] private RectTransform code3ScrollingPanel;

    [SerializeField] private GameObject[] code1Buttons;
    [SerializeField] private GameObject[] code2Buttons;
    [SerializeField] private GameObject[] code3Buttons;

    [SerializeField] private RectTransform code1Center;
    [SerializeField] private RectTransform code2Center;
    [SerializeField] private RectTransform code3Center;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private UIVerticalScroller code1VerticalScroller;
    private UIVerticalScroller code2VerticalScroller;
    private UIVerticalScroller code3VerticalScroller;

    [SerializeField] private Text taskText;

    private int codeNumbs = 3;

    private string codeText;

    private string[] code1Names = new string[] { "print.", "debug.", "set" };
    private string[] code2Names = new string[] { "{Hello world}", "Hello World", "(Hello World)" };
    private string[] code3Names = new string[] { ":", ";", "." };

    private void InitializeCodes(int codeLength)
    {
        for (int i = 0; i < codeLength; i++)
        {

        }
    }

    private void InitializeCode1()
    {
        int current = 3;

        int[] code1 = new int[current];

        code1Buttons = new GameObject[code1.Length];

        for (int i = 0; i < code1.Length; i++)
        {
            code1[i] = i;

            GameObject clone = (GameObject)Instantiate(codeButtonPrefab, new Vector3(0, i * 80, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            clone.transform.SetParent(code1ScrollingPanel, false);
            clone.transform.localScale = new Vector3(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = code1Names[i];
            clone.name = "" + code1[i];
            clone.AddComponent<CanvasGroup>();
            code1Buttons[i] = clone;
        }

    }

    private void InistializeCode2()
    {
        int current = 3;

        int[] code2 = new int[current];

        code2Buttons = new GameObject[code2.Length];

        for (int i = 0; i < code2.Length; i++)
        {
            code2[i] = i;

            GameObject clone = (GameObject)Instantiate(codeButtonPrefab, new Vector3(0, i * 80, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            clone.transform.SetParent(code2ScrollingPanel, false);
            clone.transform.localScale = new Vector3(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = code2Names[i];
            clone.name = "" + code2[i];
            clone.AddComponent<CanvasGroup>();
            code2Buttons[i] = clone;
        }
    }

    private void InitializeCode3()
    {
        int current = 3;

        int[] code3 = new int[current];

        code3Buttons = new GameObject[code3.Length];

        for (var i = 0; i < code3.Length; i++)
        {
            code3[i] = i;
            GameObject clone = (GameObject)Instantiate(codeButtonPrefab, new Vector3(0, i * 80, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            clone.transform.SetParent(code3ScrollingPanel, false);
            clone.transform.localScale = new Vector3(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = code3Names[i];
            clone.name = "" + code3[i];
            clone.AddComponent<CanvasGroup>();
            code3Buttons[i] = clone;
        }
    }

    private void Awake()
    {
        SetCodeLength();

        InitializeCode1();
        InistializeCode2();
        InitializeCode3();

        code1VerticalScroller = new UIVerticalScroller(code1ScrollingPanel, code1Buttons, code1Center);
        code2VerticalScroller = new UIVerticalScroller(code2ScrollingPanel, code2Buttons, code2Center);
        code3VerticalScroller = new UIVerticalScroller(code3ScrollingPanel, code3Buttons, code3Center);

        code1VerticalScroller.Start();
        code2VerticalScroller.Start();
        code3VerticalScroller.Start();
    }

    private void Update()
    {
        code1VerticalScroller.Update();
        code2VerticalScroller.Update();
        code3VerticalScroller.Update();

        string code1 = code1VerticalScroller.GetResults();
        string code2 = code2VerticalScroller.GetResults();
        string code3 = code3VerticalScroller.GetResults();

        codeText = code1 + code2 + code3;
    }
    private void SetCodeLength()
    {
        float width = container.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2((width - (codeNumbs * 40)) / codeNumbs, 200);
        container.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

    public void Run()
    {
        if (codeText == taskText.text.ToString())
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }

    public void CloseScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}