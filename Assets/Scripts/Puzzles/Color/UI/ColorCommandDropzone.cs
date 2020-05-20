using DN.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// This dropzone sets the parent of the dropped object as the current one, 
	/// and properly resets it once it's removed
	/// </summary>
	public partial class ColorCommandDropzone : MonoBehaviour, IDroppable
	{
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
