using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class MazeDraggableItem : DraggableItem, IEndDragHandler
	{
		public GameObject ParentObject { get; private set; }
		public event Action<MazeDraggableItem> IsDestroyedEvent;
		private float yOffset;
		private float height;

		protected virtual void Start()
		{
			height = GetComponent<RectTransform>().rect.height;
		}
	
		public override void OnEndDrag(PointerEventData eventData)
		{
			canvasGroup.alpha = 1f;
			canvasGroup.blocksRaycasts = true;

			IDroppable dropZone = null;

			foreach (RaycastHit2D hit in GetBoxCastHits())
			{
				if (hit.transform.GetComponent<IDroppable>() != null 
				&& hit.transform != transform
				&& !GetAllChildren().Contains(hit.transform.gameObject))
				{
					if (hit.transform.GetComponent<BlockDropZone>() == null)
					{
						dropZone = hit.transform.GetComponent<IDroppable>();
					}
					else if (hit.transform.GetComponent<BlockDropZone>().CurrentObj == null)
					{
						hit.transform.GetComponent<IDroppable>().Drop(this);
					}
				}
			}
			if (dropZone != null)
			{
				dropZone.Drop(this);
				return;
			}
			DestroyAllChildren();
		}

		private List<GameObject> GetAllChildren()
		{
			BlockDropZone dropZone = GetComponent<BlockDropZone>();
			if (dropZone.CurrentObj == null)
				return new List<GameObject>();

			List<GameObject> children = dropZone.CurrentObj.GetComponent<MazeDraggableItem>().GetAllChildren();
			children.Add(dropZone.CurrentObj.gameObject);
			return children;
		}


		public void DestroyAllChildren()
		{
			(GetComponent<BlockDropZone>().CurrentObj as MazeDraggableItem)?.DestroyAllChildren();
			SpawnBlock.DeleteBlock();
			IsDestroyedEvent?.Invoke(this);
			Destroy(gameObject);
		}

		public void SetParent(GameObject parent, float offset)
		{
			ParentObject = parent;
			yOffset = offset;
		}

		private void LateUpdate()
		{
			if (ParentObject != null)
			{
				transform.position = new Vector2(ParentObject.transform.position.x, ParentObject.transform.position.y - height - yOffset);
			}
		}
	}
}
