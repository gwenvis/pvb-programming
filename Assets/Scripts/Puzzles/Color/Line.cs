using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color
{
	[ExecuteInEditMode]
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

		private void Awake()
		{
			if(!startingNode || !endNode || !lineSprite)
			{
				Debug.LogError("Assign all elements first.", gameObject);
				return;
			}

			ColorPuzzleSettings settings = Resources.Load<ColorPuzzleSettings>("ColorPuzzleSettings");
			lineSprite.color = settings.ColorSettings.First(x => x.LineColor == lineColor).Color;
			//transform.position = startingNode.transform.position;

			Vector3 vector = endNode.transform.position - startingNode.transform.position;
			float rotation = Mathf.Atan2(
				vector.y, 
				vector.x) * Mathf.Rad2Deg;
			rotation = rotation < 0 ? rotation + 360f : rotation;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);

			Vector3 scale = lineSprite.transform.localScale;
			scale.x = Vector2.Distance(startingNode.transform.position, endNode.transform.position);
			lineSprite.transform.localScale = scale;
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
