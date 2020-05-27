using System;
using System.Collections;
using System.Diagnostics.Tracing;
using DN.LevelSelect.Player;
using UnityEngine;

namespace DN.Audio
{
	/// <summary>
	/// Handles all audio sound for the vehicle
	/// </summary>
	[RequireComponent(typeof(Vehicle))]
	public class VehicleAudio : MonoBehaviour
	{
		public enum State
		{
			Idle,
			Looping
		}

		[Header("Audio sources")] 
		[SerializeField] private AudioSource idleSource;
		[SerializeField] private AudioSource loopSource;
		[SerializeField] private AudioSource screechSource;

		[Header("Settings")] 
		[SerializeField] private float minScreech = 0.998f;
		[SerializeField] private float maxScreechVolume = 0.4f;

		private State currentState = State.Idle;
		private Vehicle vehicle;
		private bool canDrive;
		private Vector3 lastVelocity;

		private void Awake()
		{
			vehicle = GetComponent<Vehicle>();
			canDrive = vehicle.CanDrive;

			if (canDrive)
			{
				idleSource.Play();
			}
		}

		private void Update()
		{
			float maxSpeed = 57.0f;
			loopSource.pitch = vehicle.Velocity.sqrMagnitude / maxSpeed;
			idleSource.volume = 1 - vehicle.Velocity.sqrMagnitude / maxSpeed;
			Vector3 dot1 = vehicle.Velocity;
			dot1.y = 0;
			Vector3 dot2 = lastVelocity;
			dot2.y = 0;
			float dot = Vector3.Dot(dot1.normalized, dot2.normalized);
			screechSource.volume = Mathf.Lerp(0, maxScreechVolume, Mathf.Lerp(screechSource.volume, (1 - dot) / (1 - minScreech), 2 * Time.deltaTime));
			if (vehicle.Velocity.sqrMagnitude < 1) screechSource.volume = 0;
			
			lastVelocity = vehicle.Velocity;
		}

		private void OnEnable()
		{
			vehicle.VehicleMovedEvent += OnVehicleMovedEvent;
			vehicle.VehicleStoppedEvent += VehicleOnVehicleStoppedEvent;
			vehicle.CanDriveChangedEvent += VehicleOnCanDriveChangedEvent;
		}

		private void VehicleOnCanDriveChangedEvent(bool obj)
		{
			idleSource.mute = !obj;
			loopSource.mute = !obj;
		}

		private void VehicleOnVehicleStoppedEvent()
		{
			SwitchState(State.Idle);
		}

		private void OnVehicleMovedEvent()
		{
			SwitchState(State.Looping);
		}

		private void SwitchState(State carState)
		{
			if (carState == currentState)
			{
				return;
			}

			currentState = carState;

			if (carState == State.Idle)
			{
				idleSource.Play();
				loopSource.Pause();
			}
			else if (carState == State.Looping)
			{
				loopSource.Play();
			}
		}
	}
}

