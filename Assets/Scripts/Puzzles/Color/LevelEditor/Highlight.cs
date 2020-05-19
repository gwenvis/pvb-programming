using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class Highlight : MonoBehaviour
	{
		[SerializeField] private GameObject higlightObject;

		public void SetHighlight(bool active) => higlightObject.SetActive(active);
	}
}
