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
			Starting,
			Pitching,
			Looping
		}

		[Header("Audio sources")] 
		[SerializeField] private VehicleEngineIdle idleSource;
		[SerializeField] private AudioSource startSource;
		[SerializeField] private AudioSource loopPitchSource;
		[SerializeField] private AudioSource loopSource;

		[Header("Settings")] [SerializeField, Tooltip("What is the pitch shift increment that should be used?")]
		private float pitchShift = 0.2f;

		[SerializeField, Tooltip("What is the maximum amount of pitch shifting before going to the loop sound?")]
		private float maxPitchShift = 1.6f;

		[SerializeField, Tooltip("How much delay will there be between pitch shifting?")]
		private float pitchShiftTimeout = 0.1f;

		private State currentState = State.Idle;
		private Vehicle vehicle;
		private bool canDrive;

		private Coroutine coroutine;

		private void Awake()
		{
			vehicle = GetComponent<Vehicle>();
			idleSource = GetComponent<VehicleEngineIdle>();
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
		}

		private void OnEnable()
		{
			vehicle.VehicleMovedEvent += OnVehicleMovedEvent;
			vehicle.VehicleStoppedEvent += VehicleOnVehicleStoppedEvent;
			vehicle.CanDriveChangedEvent += VehicleOnCanDriveChangedEvent;
		}

		private void VehicleOnCanDriveChangedEvent(bool obj)
		{
			idleSource.Mute(!obj);
			loopSource.mute = !obj;
			startSource.mute = !obj;
			loopPitchSource.mute = !obj;
		}

		private void VehicleOnVehicleStoppedEvent()
		{
			SwitchState(State.Idle);
		}

		private void OnVehicleMovedEvent()
		{
			SwitchState(State.Starting);
		}

		private void StopAllOtherSources()
		{
			idleSource.Stop();
			loopSource.Stop();
			loopPitchSource.Stop();
		}

		private void SwitchState(State carState)
		{
			if (carState == currentState)
			{
				return;
			}

			currentState = carState;
			StopAllOtherSources();
			if(coroutine != null) StopCoroutine(coroutine);

			if (carState == State.Idle)
			{
				idleSource.Play();
			}
			else if (carState == State.Looping)
			{
				loopSource.Play();
			}
			else if (carState == State.Starting)
			{
				startSource.Stop();
				startSource.Play();
				SwitchState(State.Looping);
			}
		}
	}
}

