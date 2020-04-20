using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Coroutine coroutine;
    private RectTransform rectTransform;
    public Vector3 startpos;
    public Vector3 endpos;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();


        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
       // rectTransform.anchoredPosition = Vector3.Lerp(startpos, endpos, 3);
        yield return null;
    }
}
