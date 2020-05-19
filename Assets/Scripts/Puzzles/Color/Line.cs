using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// The line holds information of the starting node and end node.
	/// On start it will rotate to the correct point, and also add an arrow if needed.
	/// </summary>
	public class Line : MonoBehaviour
	{
		[Serializable]
		public struct LineSprite
		{
			[field: SerializeField] public LineColor Color { get; private set; }
			[field: SerializeField] public Sprite Line { get; private set; }
			[field: SerializeField] public Sprite LineWithArrow { get; private set; }
		}
		
		public LineData Data { get; private set; }

		[SerializeField] private LineSprite[] lines;
		private LineSprite currentLineSprite;
		private Image spriteRenderer;

		private void Awake()
		{
			if (Data.StartingNode == null || Data.EndNode == null)
			{
				Debug.LogError("Assign all elements first.", gameObject);
				return;
			}

			spriteRenderer = GetComponentInChildren<Image>();

			UpdateLineSprite();
			float rotation = SetRotation();
			SetLength();
		}

		public void InitializeData(LineData data)
		{
			if (Data is null)
			{
				Data = data;
				currentLineSprite = lines.FirstOrDefault(x => x.Color == data.LineColor);
			}
		}

		public void SetPosition(Vector3 position, int siblings, int index)
		{
			// todo: set the position to and so forth.
			SetRotation();
			SetLength();
		}
		
		private void UpdateLineSprite()
		{
			spriteRenderer.sprite = Data.CanTraverseBothWays ? currentLineSprite.Line : currentLineSprite.LineWithArrow;
		}

		private float SetRotation()
		{
			Vector3 vector = Data.EndNode.Owner.transform.position - Data.StartingNode.Owner.transform.position;
			float rotation = Mathf.Atan2(
				vector.y,
				vector.x) * Mathf.Rad2Deg;
			rotation = rotation < 0 ? rotation + 360f : rotation;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
			return rotation;
		}

		private float SetLength()
		{
			Canvas canvas = GetComponentInParent<Canvas>();

			Vector3 scale = spriteRenderer.transform.localScale;
			scale.x = Vector2.Distance(Data.StartingNode.Owner.transform.position, Data.EndNode.Owner.transform.position) / canvas.transform.lossyScale.x;
			spriteRenderer.transform.localScale = scale;
			return scale.x;
		}
	}
}
