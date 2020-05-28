using System;
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
		public event Action<DraggableItem> PickedUpItemEvent;
		public event Action<DraggableItem> DroppedItemEvent;

		public Vector2 StartPos => startPos;
		[SerializeField] private Canvas canvas;

		protected CanvasGroup canvasGroup;
		private RectTransform rectTransform;
		private Vector2 startPos;

		private void Awake()
		{
			startPos = transform.position;
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();

			if (!canvas)
				canvas = GetComponentInParent<Canvas>();
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

		public void SetCanvas(Canvas canvas) => this.canvas = canvas;

		public virtual void OnDrag(PointerEventData eventData)
		{
			rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (canvasGroup)
			{
				canvasGroup.alpha = 1f;
				canvasGroup.blocksRaycasts = true;
			}

			RaycastHit2D[] hits = GetBoxCastHits();

			foreach (RaycastHit2D hit in hits)
			{
				hit.transform.GetComponent<IDroppable>()?.Drop(this);
			}

			DroppedItemEvent?.Invoke(this);
		}

		protected virtual RaycastHit2D[] GetBoxCastHits()
		{
			return Physics2D.BoxCastAll(
				transform.position,
				transform.GetComponent<BoxCollider2D>().size,
				0,
				transform.forward
				);
		}
    }
}
