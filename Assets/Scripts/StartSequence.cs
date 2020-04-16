using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    ItemSlot itemSlotScript0;
    ItemSlot itemSlotScript1;
    ItemSlot itemSlotScript2;

    string firstPick;
    string SecondPick;
    string ThirdPick;

    void Start()
    {
        GameObject itemSlot0 = GameObject.Find("ItemSlot0");
        GameObject itemSlot1 = GameObject.Find("ItemSlot1");
        GameObject itemSlot2 = GameObject.Find("ItemSlot2");

        itemSlotScript0 = itemSlot0.GetComponent<ItemSlot>();
        itemSlotScript1 = itemSlot1.GetComponent<ItemSlot>();
        itemSlotScript2 = itemSlot2.GetComponent<ItemSlot>();
    }

    private void Update()
    {
        getItemNames();
    }

    public void getItemNames()
    {
        if (itemSlotScript0.ItemName != "" && itemSlotScript1.ItemName != "" && itemSlotScript2.ItemName != "")
        { 
            firstPick = itemSlotScript0.ItemName;
            SecondPick = itemSlotScript1.ItemName;
            ThirdPick = itemSlotScript2.ItemName;
        }
    }

    void displaysequence()
    {
        Debug.Log(firstPick + ", " + SecondPick + ", " + ThirdPick);
    }
}
