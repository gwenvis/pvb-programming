using UnityEngine;

namespace DN
{
	/// <summary>
	/// Class for player movement in maze puzzle.
	/// </summary>
	public class MazePlayerMovement : MonoBehaviour
	{
		public Vector2 currentPosition { get; private set; }
		private Vector2 orientation = new Vector2(0,1);

		public void SetPosition(int x, int y)
		{
			currentPosition = new Vector2(x, y);
			transform.position = GetPositionOnGrid(x, y);
		}

		public void Turn(float degrees)
		{
			transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + degrees));
			orientation = transform.forward;
			Debug.Log(transform.right);
		}
		
		private void SetOrientation(float degrees)
		{
			
		}

		private Vector3 GetPositionOnGrid(int x, int y)
		{
			return new Vector3(
						transform.position.x + 0.5f + x * 80,
						transform.position.y + 0.5f - y * 80,
						1.0f);
		}
	}
}
