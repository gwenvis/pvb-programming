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
		public LineColor LineColor => lineColor;
		public Node StartingNode => startingNode;
		public Node EndNode => endNode;
		public bool CanTraverseBothWays => canTraverseBothWays;

		[SerializeField] private LineColor lineColor;
		[SerializeField] private Node startingNode;
		[SerializeField] private Node endNode;
		[SerializeField] private bool canTraverseBothWays;
		[SerializeField] private Image lineSprite;

		private GameObject arrow;

		private void Awake()
		{
			if (!startingNode || !endNode || !lineSprite)
			{
				Debug.LogError("Assign all elements first.", gameObject);
				return;
			}

			arrow = Resources.Load<GameObject>("Line Pointer");

			UnityEngine.Color color = SetColor();
			float rotation = SetRotation();
			SetLength();
			CreateArrow(rotation, color);
		}

		private UnityEngine.Color SetColor()
		{
			ColorPuzzleSettings settings = Resources.Load<ColorPuzzleSettings>("ColorPuzzleSettings");
			lineSprite.color = settings.ColorSettings.First(x => x.LineColor == lineColor).Color;
			return lineSprite.color;
		}

		private float SetRotation()
		{
			Vector3 vector = endNode.transform.position - startingNode.transform.position;
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

			Vector3 scale = lineSprite.transform.localScale;
			scale.x = Vector2.Distance(startingNode.transform.position, endNode.transform.position) / canvas.transform.lossyScale.x;
			lineSprite.transform.localScale = scale;
			return scale.x;
		}

		private void CreateArrow(float rotation, UnityEngine.Color color)
		{
			if(CanTraverseBothWays)
					return;

			GameObject arrow = Instantiate(this.arrow, gameObject.transform);
			arrow.GetComponent<Image>().color = color;
			arrow.transform.eulerAngles = new Vector3(0, 0, rotation - 90);

			arrow.transform.position = 
				endNode.transform.position - 
				(endNode.transform.position - startingNode.transform.position).normalized * 
				35.0f;
		}

#if UNITY_EDITOR
		public void ConnectNode(Node node, bool startingNode)
		{
			if (startingNode)
				this.startingNode = node;
			else
				this.endNode = node;
		}

		public void DisconnectNode(bool startingNode)
		{
			if (startingNode)
				this.startingNode = null;
			else
				this.endNode = null;
		}

		public void SetTraverseBothWays(bool canTraverseBothWays)
		{
			this.canTraverseBothWays = canTraverseBothWays;
		}
#endif
	}
}
