using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private List<Transform> touchingItemSlots;
    private Vector2 startPos;
    private Transform myParent;

    private void Awake()
    {
        startPos = transform.position;
        touchingItemSlots = new List<Transform>();
    }

    public void PickUp()
    {
        transform.parent = myParent;
        touchingItemSlots.Clear();
        transform.localScale = new Vector3(6f, 6f, 6f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void Drop()
    {
        transform.localScale = new Vector3(5f, 5f, 5f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        Vector2 newPos;
        if (touchingItemSlots.Count == 0)
        {
            transform.position = startPos;
            transform.parent = myParent;
            return;
        }

        var currentItemSlot = touchingItemSlots[0];
        if(touchingItemSlots.Count == 1)
        {
            newPos = currentItemSlot.position;
        }
        else
        {
            var distance = Vector2.Distance(transform.position, touchingItemSlots[0].position);

            foreach(Transform itemSlot in touchingItemSlots)
            {
                if(Vector2.Distance(transform.position, itemSlot.position) < distance)
                {
                    currentItemSlot = itemSlot;
                    distance = Vector2.Distance(transform.position, itemSlot.position);
                }
            }
            newPos = currentItemSlot.position;
        }

        //check if the itemslot is not occupied by another item
        if(currentItemSlot.childCount != 0)
        {
            transform.position = startPos;
            transform.parent = myParent;
            return;
        }
        else
        {
            transform.parent = currentItemSlot;
            StartCoroutine(SnapIntoPos(transform.position, newPos));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<ItemSlot>()) return;
        if (!touchingItemSlots.Contains(other.transform))
        {
            touchingItemSlots.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<DraggableItem>()) return;
        if (touchingItemSlots.Contains(other.transform))
        {
            touchingItemSlots.Remove(other.transform);
        }
    }

    IEnumerator SnapIntoPos(Vector2 startPos, Vector2 endPos)
    {
        float duration = 0.2f;
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPos;
    }
}