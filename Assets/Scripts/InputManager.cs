using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject; 

    Vector2 CurrentTouchPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private bool HasInput
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }

    void Update()
    {
        if (HasInput)
        {
            PickupAndDrag();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
    }
    
    private void PickupAndDrag()
    {
        var inputPos = CurrentTouchPos;
        if (draggingItem)
        {
            draggedObject.transform.position = inputPos;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPos, inputPos, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.GetComponent<DraggableItem>())
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;

                    hit.transform.GetComponent<DraggableItem>().PickUp();
                }
            }
        }
    }

    void DropItem()
    {
        draggingItem = false;
        draggedObject.GetComponent<DraggableItem>().Drop();
    }
}
