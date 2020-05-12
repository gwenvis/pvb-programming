using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI
{
	{
		public event System.Action<DraggableItem> PickedUpItemEvent;
	public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
	/// </summary>
	/// This script is used if you want the item to be draggable. When the item is dropped it checks if it is dropped on top of an <see cref="IDroppable"/> object.
	/// <summary>

		public Vector2 StartPos => startPos;
		[SerializeField] private Canvas canvas;

		private CanvasGroup canvasGroup;
		private RectTransform rectTransform;
		private Vector2 startPos;

		private void Awake()
		{
			startPos = transform.position;
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			PickedUpItemEvent?.Invoke(this);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (canvasGroup)
			{
				canvasGroup.alpha = .5f;
				canvasGroup.blocksRaycasts = false;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (canvasGroup)
			{
				canvasGroup.alpha = 1f;
				canvasGroup.blocksRaycasts = true;
			}

			RaycastHit2D[] hits = Physics2D.BoxCastAll(
				transform.position,
				GetSize(),
				90,
				transform.forward
				);

			foreach (RaycastHit2D hit in hits)
			{
				hit.transform.GetComponent<IDroppable>()?.Drop(this);
			}
		}

		private Vector2 GetSize()
		{
			var boxCollider = GetComponent<BoxCollider2D>();
			return boxCollider == null ? rectTransform.sizeDelta : boxCollider.size * transform.lossyScale / 2;
		}
	}
		protected RaycastHit2D[] GetBoxCastHits()
		{
			return Physics2D.BoxCastAll(
				transform.position,
				transform.GetComponent<BoxCollider2D>().size * transform.lossyScale / 2,
				90,
				transform.forward
				);
		}

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

			foreach (RaycastHit2D hit in GetBoxCastHits())
            {
                hit.transform.GetComponent<IDroppable>()?.Drop(this);
            }
        }

		public void SetCanvas(Canvas newCanvas)
		{
			canvas = newCanvas;
		}
    }
}
