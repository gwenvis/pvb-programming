using Unity.Mathematics;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class CarBehaviour : MonoBehaviour
	{
		public Transform spherePos;

		private Transform transform;

		private void Start()
		{
			transform = GetComponent<Transform>();
		}

		private void Update()
		{
			//transform.rotation.x = new Vector3(0, transform.rotation.y, transform.rotation.z);
			transform = spherePos;
		}
	}
}
