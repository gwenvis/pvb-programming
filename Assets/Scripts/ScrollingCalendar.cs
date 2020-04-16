using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

    public class ScrollingCalendar : MonoBehaviour
    {
        public RectTransform code1ScrollingPanel;
        public RectTransform code2ScrollingPanel;
        public RectTransform code3ScrollingPanel;

        public GameObject codeButtonPrefab;

        private GameObject[] code1Buttons;
        private GameObject[] code2Buttons;
        private GameObject[] code3Buttons;

        public RectTransform code1Center;
        public RectTransform code2Center;
        public RectTransform code3Center;

        UIVerticalScroller code1VerticalScroller;
        UIVerticalScroller code2VerticalScroller;
        UIVerticalScroller code3VerticalScroller;

        public Text codeText;
        public Text taskText;

        private string[] code1Names = new string[]{"print.","debug.","set"};
        private string[] code2Names = new string[]{"{Hello world}","Hello World","(Hello World)"};
        private string[] code3Names = new string[]{":",";","."};


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

        //Initialize code2
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

        // Use this for initialization
        private void Awake()
        {
            InitializeCode1();
            InistializeCode2();
            InitializeCode3();

            //Yes Unity complains about this but it doesn't matter in this case.
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

            string code3tring = code3VerticalScroller.GetResults();
            string code2tring = code2VerticalScroller.GetResults();
            string code1String = code1VerticalScroller.GetResults();

            codeText.text = code1String + "" + code2tring + "" + code3tring;
        }

        public void Run(){
            if(codeText.text == taskText.text){
                Debug.Log("WIN!");
            }
        }

        private void code3ScrollUp()
        {
            code3VerticalScroller.ScrollUp();
        }

        private void code3ScrollDown()
        {
            code3VerticalScroller.ScrollDown();
        }

        private void code2ScrollUp()
        {
            code2VerticalScroller.ScrollUp();
        }

        private void code2ScrollDown()
        {
            code2VerticalScroller.ScrollDown();
        }

        private void YearsScrollUp()
        {
            code1VerticalScroller.ScrollUp();
        }

        private void YearsScrollDown()
        {
            code1VerticalScroller.ScrollDown();
        }
    }