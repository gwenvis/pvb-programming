using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Used when an item is clicked to display a highlight.
	/// </summary>
	public class Highlight : MonoBehaviour
	{
		[SerializeField] private GameObject higlightObject;

		public void SetHighlight(bool active) => higlightObject.SetActive(active);
	}
}
