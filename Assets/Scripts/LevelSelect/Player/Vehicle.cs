using DN.Levelselect.LevelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.Levelselect.Player
{
    public class Vehicle : MonoBehaviour
    {

        [SerializeField] private Transform vehicleModel;
        [SerializeField] private Rigidbody sphere;

        [SerializeField] [Range(5.0f, 40.0f)] private float acceleration = 30f;
        [SerializeField] [Range(20.0f, 160.0f)] private float steering = 80f;
        [SerializeField] [Range(0.0f, 20.0f)] private float gravity = 10f;

        private Transform container, wheelFrontLeft, wheelFrontRight;
        [SerializeField] private Transform body;

        private float speed, speedTarget;
        private float rotate, rotateTarget;

        private bool canSteer = false;
        private bool nearGround, onGround;

        private Vector3 containerBase;

        void Awake()
        {
            container = vehicleModel.GetChild(0);
            containerBase = container.localPosition;
        }

        void Update()
        {
            Accelarate();
            WheelAndBodyTilt();
            VehicleTilt();
            Steering();

            // Stops vehicle from floating around when standing still

            if (speed == 0 && sphere.velocity.magnitude < 4f)
            {
                sphere.velocity = Vector3.Lerp(sphere.velocity, Vector3.zero, Time.deltaTime * 2.0f);
                canSteer = false;
            }
            else
            {
                canSteer = true;
            }
        }

        void FixedUpdate()
        {
            MovementHandler();
        }

        private void VehicleTilt()
        {
            float tilt = 0.0f;

            container.localPosition = containerBase + new Vector3(0, Mathf.Abs(tilt) / 2000, 0);
            container.localRotation = Quaternion.Slerp(container.localRotation, Quaternion.Euler(0, rotateTarget / 8, tilt), Time.deltaTime * 10.0f);
        }

        private void WheelAndBodyTilt()
        {
            if (wheelFrontLeft != null)
            {
                wheelFrontLeft.localRotation = Quaternion.Euler(0, rotateTarget / 2, 0);
            }

            if (wheelFrontRight != null)
            {
                wheelFrontRight.localRotation = Quaternion.Euler(0, rotateTarget / 2, 0);
            }

            body.localRotation = Quaternion.Slerp(body.localRotation, Quaternion.Euler(new Vector3(speedTarget / 4, 0, rotateTarget / 6)), Time.deltaTime * 4.0f);
        }

        private void Accelarate()
        {
            speedTarget = Mathf.SmoothStep(speedTarget, speed, Time.deltaTime * 12f);

            speed = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                speed = acceleration;
            }

            if (Input.GetKey(KeyCode.S))
            {
                speed = -acceleration;
            }
        }

        private void Steering()
        {
            rotateTarget = Mathf.Lerp(rotateTarget, rotate, Time.deltaTime * 4f); rotate = 0f;

            if (canSteer && nearGround)
            {

                if (Input.GetKey(KeyCode.A))
                {
                    rotate = -steering;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rotate = steering;
                }

            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + rotateTarget, 0)), Time.deltaTime * 2.0f);
        }

        private void MovementHandler()
        {
            RaycastHit hitOn;
            RaycastHit hitNear;

            onGround = Physics.Raycast(transform.position, Vector3.down, out hitOn, 1.1f);
            nearGround = Physics.Raycast(transform.position, Vector3.down, out hitNear, 2.0f);

            vehicleModel.up = Vector3.Lerp(vehicleModel.up, hitNear.normal, Time.deltaTime * 8.0f);
            vehicleModel.Rotate(0, transform.eulerAngles.y, 0);

            if (nearGround)
            {
                sphere.AddForce(vehicleModel.forward * speedTarget, ForceMode.Acceleration);
            }
            else
            {
                sphere.AddForce(vehicleModel.forward * (speedTarget / 10), ForceMode.Acceleration);

                sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            }

            transform.position = sphere.transform.position + new Vector3(0, 0.35f, 0);
        }
    }
}