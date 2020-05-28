using DN.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Dropzone for blocks with more children
	/// </summary>
	public class LoopDropZone : BlockDropZone, IDroppable
	{
		[SerializeField] private RectTransform sideTransform;
		protected List<DraggableItem> childObjects = new List<DraggableItem>();
		protected BoxCollider2D boxCollider;
		protected float yOffset;
		protected float originalColliderYOffset;
		private float xOffset = 4f;
		private Dictionary<DraggableItem, int> listPositions;
		private Dictionary<int, Type> types;

		protected virtual void Start()
		{
			GetSideTransform();
			listPositions = new Dictionary<DraggableItem, int>();
			boxCollider = GetComponent<BoxCollider2D>();
			yOffset = GetComponent<RectTransform>().rect.height * 2;
			originalColliderYOffset = boxCollider.offset.y;
			types = new Dictionary<int, Type>();
		}

		public new void Drop(DraggableItem droppedObject)
		{
			if (GetComponent<MazeDraggableItem>())
			{
				droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, GetComponent<RectTransform>().rect.height, sideTransform.rect.width/2 + xOffset);
			}
			else
			{
				droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, GetComponent<RectTransform>().rect.height*1.5f, sideTransform.rect.width/2 + xOffset + GetComponent<RectTransform>().rect.width/2);
			}
			droppedObject.transform.SetParent(transform.parent);
			types.Add(childObjects.Count, (droppedObject as MazeDraggableItem).DropZoneHolder.GetComponent<IDroppable>().GetType());
			droppedObject.GetComponent<BlockDropZone>().SetLayer(Layer + 1);
			Destroy(droppedObject.GetComponentsInChildren<BlockDropZone>().Last());
			SetBlockParent(droppedObject);
			listPositions.Add(droppedObject, childObjects.Count);
			childObjects.Add(droppedObject);
			ResizeBlock();
			droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
			DropChildObjects(droppedObject as MazeDraggableItem);
			(droppedObject as MazeDraggableItem).SetNearestLoopObject(this);
		}

		private void OnPickedUpItemEvent(DraggableItem obj)
		{
			Component item = (obj as MazeDraggableItem).DropZoneHolder.AddComponent(types[listPositions[obj]]);
			if (item.GetType() == typeof(LoopDropZone))
				(item as LoopDropZone).GetSideTransform();
			GetObjectChilds(obj);
			obj.GetComponent<MazeDraggableItem>().SetParent(null, 0);
			RemoveFromAllLists(obj);
			(obj as MazeDraggableItem).SetNearestLoopObject(null);
			obj.PickedUpItemEvent -= OnPickedUpItemEvent;
			ResizeBlock();
		}

		private void DropChildObjects(MazeDraggableItem droppedObject)
		{
			if (droppedObject.DropZoneHolder.GetComponent<BlockDropZone>().CurrentObj)
				Drop(droppedObject.DropZoneHolder.GetComponent<BlockDropZone>().CurrentObj);
		}

		private void SetBlockParent(DraggableItem droppedObject)
		{
			if (childObjects.Count == 0)
				return;

			MazeDraggableItem draggableItem = (childObjects[childObjects.Count - 1] as MazeDraggableItem);
			droppedObject.GetComponent<MazeDraggableItem>().SetParent(draggableItem.DropZoneHolder, 
				draggableItem.RelativeHeight + draggableItem.HolderYOffset, draggableItem.HolderXOffset);	
		}

		private void GetObjectChilds(DraggableItem obj)
		{
			int listPosition = listPositions[obj];
			List<DraggableItem> currentItems = childObjects.ToList();
			for(int i = listPosition + 1; i < currentItems.Count; i++)
			{
				(currentItems[i - 1] as MazeDraggableItem).DropZoneHolder.GetComponentInChildren<BlockDropZone>().Drop(currentItems[i]);
				currentItems[i].PickedUpItemEvent -= OnPickedUpItemEvent;
				(currentItems[i] as MazeDraggableItem).DropZoneHolder.AddComponent(types[listPositions[currentItems[i]]]);
				(currentItems[i] as MazeDraggableItem).SetNearestLoopObject(null);
				RemoveFromAllLists(currentItems[i]);
			}
		}

		private void RemoveFromAllLists(DraggableItem obj)
		{
			types.Remove(listPositions[obj]);
			listPositions.Remove(obj);
			childObjects.Remove(obj);
		}

		public List<DraggableItem> GetChildObjects()
		{
			return childObjects;
		}

		public void SetChildObjects(List<DraggableItem> items)
		{
			childObjects = items;
		}

		public virtual void ResizeBlock()
		{
			float height= 0;
			MazeDraggableItem mazeDraggableItem = GetComponentInParent<MazeDraggableItem>();
			if (childObjects.Count == 0)
			{
				height = mazeDraggableItem.RelativeHeight;
			}
			foreach(DraggableItem item in childObjects)
			{
				height += (item as MazeDraggableItem).Height;
			}
			mazeDraggableItem.SetHeight(height + yOffset);
			sideTransform.sizeDelta = new Vector2(height, sideTransform.sizeDelta.y);
			boxCollider.size = new Vector2(boxCollider.size.x, height + yOffset/2);
			boxCollider.offset = new Vector2(boxCollider.offset.x, originalColliderYOffset - height / 2 + yOffset / 4);
			mazeDraggableItem.NearestLoopObject?.ResizeBlock();
		}

		private void GetSideTransform()
		{
			foreach(RectTransform transform in GetComponentsInChildren<RectTransform>())
			{
				if(transform.name == "Side")
				{
					sideTransform = transform;
					break;
				}
			}
		}

		private void OnDestroy()
		{
			foreach(DraggableItem item in childObjects)
				item.PickedUpItemEvent -= OnPickedUpItemEvent;
		}
	}
}
