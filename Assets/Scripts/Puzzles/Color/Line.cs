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
		public LineData Data { get; private set; }
		
		[SerializeField] private Image lineSprite;
		[SerializeField] private Image lineWithArrowSprite;

		private GameObject arrow;
		
		private void Awake()
		{
			if (Data.StartingNode == null || Data.EndNode == null || lineSprite == null)
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

		public void InitializeData(LineData data)
		{
			if (Data is null)
			{
				Data = data;
			}
		}
		
		private UnityEngine.Color SetColor()
		{
			ColorPuzzleSettings settings = Resources.Load<ColorPuzzleSettings>("ColorPuzzleSettings");
			lineSprite.color = settings.ColorSettings.First(x => x.LineColor == Data.LineColor).Color;
			return lineSprite.color;
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

			Vector3 scale = lineSprite.transform.localScale;
			scale.x = Vector2.Distance(Data.StartingNode.Owner.transform.position, Data.EndNode.Owner.transform.position) / canvas.transform.lossyScale.x;
			lineSprite.transform.localScale = scale;
			return scale.x;
		}

		private void CreateArrow(float rotation, UnityEngine.Color color)
		{
			if(Data.CanTraverseBothWays)
					return;

			GameObject lArrow = Instantiate(this.arrow, gameObject.transform);
			lArrow.GetComponent<Image>().color = color;
			lArrow.transform.eulerAngles = new Vector3(0, 0, rotation - 90);

			var position = Data.EndNode.Owner.transform.position;
			lArrow.transform.position = 
				position - 
				(position - Data.StartingNode.Owner.transform.position).normalized * 
				42.0f;
		}
	}
}
