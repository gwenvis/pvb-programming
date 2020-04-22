using DN.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class ColorCommandDropzone : MonoBehaviour, IDroppable
	{
		private struct Item
		{
			public DraggableItem draggableItem;
			public ColorCommand colorCommand;
			public Transform previousParent;
		}

		public IEnumerable<ColorCommand> CommandQueue => colors.Select(x=>x.colorCommand);

		private List<Item> colors = new List<Item>();

		public void Drop(DraggableItem droppedObject)
		{
			colors.Add(new Item
			{
				draggableItem = droppedObject,
				previousParent = droppedObject.transform.parent,
				colorCommand = droppedObject.GetComponent<ColorCommand>()
			});

			droppedObject.transform.parent = transform;
			droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
		}

		private void OnPickedUpItemEvent(DraggableItem item)
		{
			Item linqItem = colors.FirstOrDefault(x => x.draggableItem == item);
			linqItem.draggableItem.transform.parent = linqItem.previousParent ?? null;
			colors.Remove(linqItem);
			item.PickedUpItemEvent -= OnPickedUpItemEvent;
		}
	}
}
