using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.LevelSelect.CameraMovement
{
    /// <summary>
    /// THis is the camera movement to follow the car
    /// </summary>
    public class VehicleCamera : MonoBehaviour
    {

        [SerializeField] private Transform cameraRig;

        [SerializeField] [Range(1, 20)] private float followSpeed = 16;

        private Vector3 cameraPositionOffset;

        private Camera camera;

        void Awake()
        {
            cameraPositionOffset = cameraRig.localPosition;
            camera = cameraRig.GetChild(0).GetComponent<Camera>();
        }

        void LateUpdate()
        {
            cameraRig.position = Vector3.Lerp(cameraRig.position, transform.position + cameraPositionOffset, Time.deltaTime * followSpeed);
        }
    }
}