using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Dropzone for if else block
	/// </summary>
	public class IfElseDropzone : LoopDropZone
	{
		[SerializeField] private IfElseDropzone otherDropZone;
		private float offset;
		private bool isElse;

		protected override void Start()
		{
			base.Start();
			offset = GetComponent<RectTransform>().rect.height;
			isElse = !GetComponent<DraggableItem>();
		}

		public override void ResizeBlock()
		{
			base.ResizeBlock();
			MazeDraggableItem mazeDraggableItem = GetComponentInParent<MazeDraggableItem>();
			mazeDraggableItem.SetHeight(boxCollider.size.y + otherDropZone.boxCollider.size.y + offset);
			if(isElse)
				boxCollider.offset = new Vector2(boxCollider.offset.x, originalColliderYOffset + GetComponent<BoxCollider2D>().size.y/2 - yOffset / 2);
			mazeDraggableItem.NearestLoopObject?.ResizeBlock();
		}

		public override void SetLayer(int layer)
		{
			Layer = layer;
			otherDropZone.Layer = layer;
		}
	}
}
