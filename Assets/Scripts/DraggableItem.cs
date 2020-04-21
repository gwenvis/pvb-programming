using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI
{
    /// <summary>
    /// This script is used if you want the item to be draggable. When the item is dropped it checks if it is dropped on top of an <see cref="IDroppable"/> object.
    /// </summary>
    public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Vector2 StartPos => startPos;
        [SerializeField] private Canvas canvas;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        public event System.Action PickedUpItemEvent;

        private Vector2 startPos;

        private void Awake()
        {
            startPos = transform.position;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PickedUpItemEvent?.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .5f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, transform.GetComponent<BoxCollider2D>().size * transform.lossyScale / 2, 90, transform.forward);
            foreach (RaycastHit2D hit in hits)
            {
                hit.transform.GetComponent<IDroppable>()?.Drop(this);
            }
        }
    }
}