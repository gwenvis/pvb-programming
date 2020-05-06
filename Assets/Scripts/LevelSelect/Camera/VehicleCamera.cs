using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VehicleCamera : MonoBehaviour{
	
	[SerializeField] private Transform cameraRig;
	
	[SerializeField][Range(1, 20)] private float followSpeed	= 16;

	private Vector3 cameraPositionOffset;
	
	private Camera camera;
	
	void Awake(){
		
		// Remember offset set in editor
		
		cameraPositionOffset = cameraRig.localPosition;
		
		// Get camera
		
		camera = cameraRig.GetChild(0).GetComponent<Camera>();
	}
	
	void FixedUpdate(){
		
		// Camera follow
		
		cameraRig.position = Vector3.Lerp(cameraRig.position, transform.position + cameraPositionOffset, Time.deltaTime * followSpeed);
		
	}
	
}