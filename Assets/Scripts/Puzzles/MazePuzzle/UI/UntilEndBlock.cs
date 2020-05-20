using DN.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for UntilEnd Block
	/// </summary>
	public class UntilEndBlock : MazeDraggableItem, IEndDragHandler, IMovePlayerBlock
	{
		MazeFunctions function = MazeFunctions.UntilEnd;
		public float UpperBlockHeight { get; private set; }

		protected override void Start()
		{
			base.Start();
			Height = GetComponent<BoxCollider2D>().size.y + GetComponent<RectTransform>().rect.height;
			RelativeHeight = GetComponent<RectTransform>().rect.height;
			UpperBlockHeight = GetComponent<RectTransform>().rect.height;
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
				
			if(blockDropZoneHits.Length > 0)
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
			foreach (MazeDraggableItem item in items)
			{
				item.DestroyAllChildren();
			}
			if(DropZoneHolder.GetComponent<BlockDropZone>())
				(DropZoneHolder.GetComponent<BlockDropZone>().CurrentObj as MazeDraggableItem)?.DestroyAllChildren();
			SpawnBlock.DeleteBlock();
			CallIsDestroyEvent();
			Destroy(gameObject);
		}

		public MazeFunctions GetMazeFunctions()
		{
			return function;
		}

		protected override void Update()
		{
			if (ParentObject != null)
			{
				transform.position = new Vector2(ParentObject.transform.position.x + xOffset, ParentObject.transform.position.y - yOffset);
			}
			ExtDebug.BoxCast2D(transform.position,
				new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height),
				0,
				transform.forward, 10, 0);
		}

		public override List<(MazeFunctions function, MazeDraggableItem item)> GetQueue()
		{
			foreach(MazeDraggableItem item in GetComponent<LoopDropZone>().GetChildObjects())
			{
				functionQueue.Add((item.GetComponent<IMovePlayerBlock>().GetMazeFunctions(), item));
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
