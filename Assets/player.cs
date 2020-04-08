using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button playButton;
    [SerializeField] private TextMeshProUGUI voltageText;
    [SerializeField] private TextMeshProUGUI buttonActivatedText;

    private queueue que;
    private leveleeGenerator level;
    private Coroutine coroutine;
    private List<(int, int)> acquiredVoltages = new List<(int, int)>();
    private bool turnedOnButton;
    private int voltage;

    private void Start()
    {
        que = FindObjectOfType<queueue>();
        level = FindObjectOfType<leveleeGenerator>();
        playButton.onClick.AddListener(() =>
        {
            que.ResetIndex();
            buttonActivatedText.text = " Voltage button not actived.";
            voltageText.text = "0";
            turnedOnButton = false;
            acquiredVoltages.Clear();

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(Go());
        });
    }

    private IEnumerator Go()
    {
        // place player on start position
        transform.position = level.GetStartingPosition(out int x, out int y);

        yield return new WaitForSeconds(1);

        directionblock sd;
        while((sd = que.GetNerxtBlock()) != null)
        {
            sd.Acitvati();
            int tempX = x;
            int tempY = y;
            switch(sd.Direction)
            {
                case diRIerection.downer:
                    tempX++;
                    break;
                case diRIerection.left:
                    tempY--;
                    break;
                case diRIerection.right:
                    tempY++;
                    break;
                case diRIerection.uppy:
                    tempX--;
                    break;
            }

            int tile = level.Level[tempX][tempY];

            if(tile == 6)
            {
                tempX = x;
                tempY = y;
            }
            if(tile == 3)
            {
                turnedOnButton = true;
                buttonActivatedText.text = " Voltage Button activated.";
            }
            if (tile == 1 || (tile == 2 && turnedOnButton))
            {
                if (!acquiredVoltages.Contains((tempX, tempY)))
                {
                    acquiredVoltages.Add((tempX, tempY));
                    voltage++;
                    voltageText.text = voltage.ToString();
                }
            }
            if(tile == 4 && voltage == level.minPowerNeeded)
            {
                voltageText.text = "YOU ARE WINNED";
            }
            else if(tile == 4)
            {
                voltageText.text = "You are losser";
            }

            transform.position = level.GetPositionOnGrid(tempX, tempY);

            x = tempX;
            y = tempY;

            yield return new WaitForSeconds(1.5f);
            sd.Decec();
        }
    }
}
