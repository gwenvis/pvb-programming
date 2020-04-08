using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class queueue : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private List<GameObject> l = new List<GameObject>();
    private int currentIndex;

    protected void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int ok = i;
            buttons[i].onClick.AddListener(() =>
            {
                l.Add(Instantiate(buttons[ok].gameObject, transform));
            });
        }
    }

    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Clear()
    {
        foreach(var f in l)
            Destroy(f);

        l.Clear();
    }

    public directionblock GetNerxtBlock()
    {
        if(currentIndex > transform.childCount)
        {
            return null;
        }

        var s = transform.GetChild(currentIndex);

        currentIndex++;
        return s.GetComponent<directionblock>();
    }

    public void ResetIndex()
    {
        currentIndex = 0;
    }
}
