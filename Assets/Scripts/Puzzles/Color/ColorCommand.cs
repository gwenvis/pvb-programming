using UnityEngine;

namespace DN.Puzzle.Color
{
	public class ColorCommand : MonoBehaviour
	{
		public LineColor Color => color;

		[SerializeField] private LineColor color;
	}
}
