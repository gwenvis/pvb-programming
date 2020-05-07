using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.Levelselect.PhysicsObjects
{
    /// <summary>
    /// This script can be applied on an object to make it handle the physiscs collision with the car so it can move around or be bumped into with a fun effect.
    /// </summary>
    public class PhysicsObject : MonoBehaviour
    {
        void Awake()
        {

            GetComponent<MeshCollider>().convex = true;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;

        }

        public void Hit(Vector3 velocity)
        {

            velocity = Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.up) * velocity;
            velocity = Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.right) * velocity;

            GetComponent<Rigidbody>().AddForce(velocity * Random.Range(30, 60));
            GetComponent<Rigidbody>().AddTorque(velocity * Random.Range(30, 60));

            gameObject.layer = 2;

        }
    }
}