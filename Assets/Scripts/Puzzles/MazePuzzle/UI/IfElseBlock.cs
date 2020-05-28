using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for if else blocks
	/// </summary>
	public class IfElseBlock : IfBlock
	{
		protected override void Start()
		{
			base.Start();
			Height += elseObject.gameObject.GetComponent<BoxCollider2D>().size.y;
		}

		public override void DestroyAllChildren()
		{
			List<MazeDraggableItem> items = elseObject.GetChildObjects().Cast<MazeDraggableItem>().ToList();
			items.ForEach(x => x.DestroyAllChildren());
			base.DestroyAllChildren();
		}
	}
}
