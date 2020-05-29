using DN.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// General class for all blocks that have blocks inside them.
	/// </summary>
	public class LoopBlockDraggableItem : MazeDraggableItem, IEndDragHandler
	{
		protected override void Start()
		{
			base.Start();
			GetHeight();
			RelativeHeight = GetComponent<RectTransform>().rect.height;
			HolderXOffset = DropZoneHolder.GetComponent<RectTransform>().rect.width / 2;
			HolderYOffset = DropZoneHolder.GetComponent<RectTransform>().rect.height / 2;
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			canvasGroup.alpha = 1f;
			canvasGroup.blocksRaycasts = true;

			RaycastHit2D[] hits = GetBoxCastHits();

			IDroppable dropzone = null;

			RaycastHit2D[] blockDropZoneHits = hits.ToArray().Where(x => x.transform.GetComponent<BlockDropZone>() != null
				&& x.transform != transform
				&& !GetComponent<LoopDropZone>().GetChildObjects().Contains(x.transform.GetComponent<DraggableItem>())).ToArray();

			if (blockDropZoneHits.Length > 0)
				dropzone = blockDropZoneHits.OrderByDescending(x => x.transform.GetComponent<BlockDropZone>().Layer).First().transform.GetComponent<IDroppable>();

			hits = hits.Where(x => x.transform != transform && !GetComponent<LoopDropZone>().GetChildObjects().Contains(x.transform.GetComponent<DraggableItem>())).ToArray();

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

		public override void DestroyAllChildren()
		{
			List<MazeDraggableItem> items = GetComponent<LoopDropZone>().GetChildObjects().Cast<MazeDraggableItem>().ToList();
			items.ForEach(x => x.DestroyAllChildren());
			if (DropZoneHolder.GetComponent<BlockDropZone>())
				(DropZoneHolder.GetComponent<BlockDropZone>().CurrentObj as MazeDraggableItem)?.DestroyAllChildren();
			CallIsDestroyEvent();
			Destroy(gameObject);
		}

		protected override void Update()
		{
			if (ParentObject != null)
			{
				transform.position = new Vector2(ParentObject.transform.position.x + xOffset, ParentObject.transform.position.y - yOffset);
			}
		}

		public override void GetHeight()
		{
			Height = GetComponent<BoxCollider2D>().size.y + GetComponent<RectTransform>().rect.height;
		}

		public override List<(MazeFunctions function, MazeDraggableItem item)> GetQueue()
		{
			functionQueue = new List<(MazeFunctions, MazeDraggableItem)>();
			foreach (MazeDraggableItem item in GetComponent<LoopDropZone>().GetChildObjects())
			{
				functionQueue.Add((item.GetComponent<IMovePlayerBlock>().GetMazeFunction(), item));
			}
			return base.GetQueue();
		}

		protected override RaycastHit2D[] GetBoxCastHits()
		{
			return Physics2D.BoxCastAll(
				transform.position,
				new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height),
				0,
				transform.forward
				);
		}
	}
}
