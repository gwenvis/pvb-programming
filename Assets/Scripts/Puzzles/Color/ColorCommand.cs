using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Color command holds data about what command the block is
	/// </summary>
	public class ColorCommand : MonoBehaviour
	{
		public LineColor Color => color;

		[SerializeField] private LineColor color;
	}
}
