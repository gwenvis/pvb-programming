using UnityEngine;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class SphereMovement : MonoBehaviour
	{
		[SerializeField] private float speed;

		private Rigidbody rb;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			float horizontalMovement = Input.GetAxis("Horizontal");
			float verticalMovement = Input.GetAxis("Vertical");

			Vector3 movementForce = new Vector3(horizontalMovement, 0.0f, verticalMovement);

			rb.AddForce(movementForce * speed);
		}
	}
}
