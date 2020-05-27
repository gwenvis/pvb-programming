using UnityEngine;

namespace DN
{
	/// <summary>
	/// Shows the dimensions and position of a box collider
	/// </summary>
	[ExecuteInEditMode]
	public class ClipBrushDisplay : MonoBehaviour
	{
		private UnityEngine.Color color = new UnityEngine.Color(0.54f, 0.11f, 0.47f, 0.47f);
		private BoxCollider boxCollider;
		
		private void Awake()
		{
			boxCollider = GetComponent<BoxCollider>();
		}

		private void OnDrawGizmos()
		{
			UnityEngine.Color o = Gizmos.color;
			Gizmos.color = color;
			var matrix = Gizmos.matrix;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawCube(boxCollider.center, boxCollider.size);
			Gizmos.color = o;
			Gizmos.matrix = matrix;
		}
	}
}
