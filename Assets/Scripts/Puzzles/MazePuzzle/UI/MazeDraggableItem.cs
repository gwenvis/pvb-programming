using DN.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// basse Class for every draggable item in the maze puzzle
	/// </summary>
	public class MazeDraggableItem : DraggableItem, IEndDragHandler
	{
		[SerializeField] private GameObject dropZoneHolder;
		public GameObject DropZoneHolder => dropZoneHolder;
		public GameObject ParentObject { get; private set; }
		public float Height { get; protected set; }
		public float RelativeHeight { get; protected set; }
		public float HolderXOffset { get; protected set; }
		public float HolderYOffset { get; protected set; }
		public LoopDropZone NearestLoopObject { get; private set; }
		public event Action<MazeDraggableItem> IsDestroyedEvent;
		protected float yOffset;
		protected float xOffset;
		protected List<(MazeFunctions function, MazeDraggableItem item)> functionQueue;

		protected virtual void Start()
		{
			functionQueue = new List<(MazeFunctions function, MazeDraggableItem item)>();
			GetHeight();
			RelativeHeight = GetComponent<RectTransform>().rect.height;
			HolderXOffset = 0;
			HolderYOffset = 0;
		}
	
		public override void OnEndDrag(PointerEventData eventData)
		{
			canvasGroup.alpha = 1f;
			canvasGroup.blocksRaycasts = true;

			RaycastHit2D[] hits = GetBoxCastHits();

			IDroppable dropzone = null;

			RaycastHit2D[] blockDropZoneHits = hits.ToArray().Where(x => x.transform.GetComponent<BlockDropZone>() != null
				&& x.transform != transform
				&& !GetAllChildren().Contains(x.transform.gameObject)).ToArray();

			if(blockDropZoneHits.Length > 0)
				dropzone = blockDropZoneHits.OrderByDescending(x => x.transform.GetComponent<BlockDropZone>().Layer).First().transform.GetComponent<IDroppable>();

			hits = hits.Where(x => x.transform != transform && !GetAllChildren().Contains(x.transform.gameObject)).ToArray();

			if (dropzone == null && hits.Length > 0)
			{
				dropzone = hits[0].transform.GetComponent<IDroppable>();
			}

			if (dropzone != null)
			{
				dropzone.Drop(this);
				return;
			}

			DestroyAllChildren();
		}

		public virtual List<GameObject> GetAllChildren()
		{
			BlockDropZone dropZone = GetComponent<BlockDropZone>();
			if (dropZone.CurrentObj == null)
				return new List<GameObject>();

			List<GameObject> children = dropZone.CurrentObj.GetComponent<MazeDraggableItem>().GetAllChildren();
			children.Add(dropZone.CurrentObj.gameObject);
			return children;
		}


		public virtual void DestroyAllChildren()
		{
			if(GetComponent<BlockDropZone>())
				(GetComponent<BlockDropZone>().CurrentObj as MazeDraggableItem)?.DestroyAllChildren();
			SpawnBlock.DeleteBlock();
			CallIsDestroyEvent();
			Destroy(gameObject);
		}

		public void SetParent(GameObject parent, float offsetY, float OffsetX = 0)
		{
			ParentObject = parent;
			yOffset = offsetY;
			xOffset = OffsetX;
		}

		public void SetHeight(float height)
		{
			Height = height;
		}

		public virtual void GetHeight()
		{
			Height = GetComponent<RectTransform>().rect.height;
		}

		public void SetNearestLoopObject(LoopDropZone obj)
		{
			NearestLoopObject = obj;
		}

		public virtual List<(MazeFunctions function, MazeDraggableItem item)> GetQueue()
		{
			return functionQueue;
		}

		protected void CallIsDestroyEvent()
		{
			IsDestroyedEvent?.Invoke(this);
		}
		
		protected virtual void Update()
		{
			if (ParentObject != null)
			{
				transform.position = new Vector2(ParentObject.transform.position.x + xOffset, ParentObject.transform.position.y - yOffset);
			}
		}
	}
}
