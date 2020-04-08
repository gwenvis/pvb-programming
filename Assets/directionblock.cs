using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionblock : MonoBehaviour
{
    public diRIerection Direction;
    public UnityEngine.UI.Image backgroundImage;
    public UnityEngine.UI.Image foregroundImage;

    public void Acitvati()
    {
        backgroundImage.color = Color.yellow;
    }

    public void Decec()
    {
        backgroundImage.color = Color.white;
    }
}

public enum diRIerection
{ 
    left,
    right,
    uppy,
    downer,
}

