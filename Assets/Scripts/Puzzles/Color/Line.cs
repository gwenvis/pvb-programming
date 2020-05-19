using System;
using System.Collections.Generic;
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

		public const float SIBLING_OFFSET = 15.0f;
		public const float OFFSET_FROM_CIRCLE = 5.0f;
		
		public LineData Data { get; private set; }

		[SerializeField] private LineSprite[] lines;
		private LineSprite currentLineSprite;
		private Image spriteRenderer;

		private void Awake()
		{
			spriteRenderer = GetComponentInChildren<Image>();

			UpdateLineSprite();
		}

		public void Start()
		{
			InitializePosition();
		}

		public void InitializePosition()
		{
			if (Data?.StartingNode == null || Data.EndNode == null)
			{
				return;
			}
			
			// get all siblings and our own index from theses siblings.
			
			var siblings = Data.StartingNode.ConnectedLines.Where(x =>
				(x.StartingNode == Data.StartingNode && x.EndNode == Data.EndNode) ||
				(x.StartingNode == Data.EndNode && x.EndNode == Data.StartingNode)).ToList();
			int myIndex = siblings.IndexOf(Data);

			SetPosition(Data.StartingNode.Owner.transform.position, siblings.Count, myIndex);
		}

		public void AssignToParent()
		{
			Data.StartingNode.AddLineData(Data);
			Data.EndNode.AddLineData(Data);
		}

		public void InitializeData(LineData data)
		{
			if (Data is null)
			{
				Data = data;
				UpdateLineSprite();
			}
		}

		public void SetPosition(Vector3 position, int siblings, int index)
		{
			// todo: set the position to and so forth.
			float radius = Data.StartingNode.Owner.GetComponent<RectTransform>().sizeDelta.x;
			
			var endOwnerPosition = Data.EndNode.Owner.transform.position;
			var startPosition = Data.StartingNode.Owner.transform.position;
			endOwnerPosition -= (endOwnerPosition - startPosition).normalized * (radius / 2 + OFFSET_FROM_CIRCLE);
			position += (endOwnerPosition - startPosition).normalized * (radius / 2 + OFFSET_FROM_CIRCLE);
			startPosition = position;
			
			Vector3 vector = (endOwnerPosition - position).normalized;
			Vector3 cross = new Vector2(vector.y, -vector.x);
			
			int dot = (index % 2) * 2 - 1;
			int dir = index - index / 2;
			int m = (siblings % 2 + 1);
			transform.position = position;
			SetRotation(endOwnerPosition);
			SetLength(startPosition, endOwnerPosition);
			transform.position = position + (dir * dot * cross / m * SIBLING_OFFSET * m);
		}
		
		public void UpdateLineSprite()
		{
			if (Data == null)
			{
				spriteRenderer.sprite = lines[0].LineWithArrow;
				return;
			}
			
			currentLineSprite = lines.FirstOrDefault(x => x.Color == Data.LineColor);
			spriteRenderer.sprite = Data.CanTraverseBothWays ? currentLineSprite.Line : currentLineSprite.LineWithArrow;
		}

		public float SetRotation(Vector3 end)
		{
			Vector3 vector = end - transform.position;
			float rotation = Mathf.Atan2(
				vector.y,
				vector.x) * Mathf.Rad2Deg;
			rotation = rotation < 0 ? rotation + 360f : rotation;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
			return rotation;
		}

		public float SetLength(Vector3 startPosition, Vector3 endPosition)
		{
			Canvas canvas = GetComponentInParent<Canvas>();

			var size = spriteRenderer.rectTransform.sizeDelta;
			float length = Vector2.Distance(startPosition, endPosition) / canvas.scaleFactor;
			size.x = length;
			spriteRenderer.rectTransform.sizeDelta = size;
			return length;
		}
	}
}
