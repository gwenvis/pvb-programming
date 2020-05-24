using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DN.Audio
{
	/// <summary>
	/// Handles the engine idle sound effect.
	/// </summary>
	public class VehicleEngineIdle : MonoBehaviour
	{
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip[] clips;
		[SerializeField] private bool avoidRepeating = true;

		private bool started;
		private float timeTillNextSound;
		private float randomPitchMin = 0.98f;
		private float randomPitchMax = 1.05f;
		private AudioClip oldAudioClip;
		private bool lowPitch = false;

		public void Play()
		{
			started = true;
			SelectRandomClip();
			StartCoroutine(AudioUpdate());
		}

		public void Stop()
		{
			started = false;
		}

		public void Mute(bool mute) => audioSource.mute = mute;

		private AudioClip SelectRandomClip()
		{
			do
			{
				audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
			} while (avoidRepeating && oldAudioClip == audioSource.clip && clips.Length > 1);

			oldAudioClip = audioSource.clip;
			return oldAudioClip;
		}

		private float GetWaitTime(AudioClip clip, float pitch) => clip.length / pitch; 

		private IEnumerator AudioUpdate()
		{
			double nextSwitch = AudioSettings.dspTime;
			audioSource.loop = true;
			audioSource.Play();
			while (started)
			{
				if (AudioSettings.dspTime < nextSwitch)
				{
					yield return null;
					continue;
				}

				float pitch = lowPitch ? randomPitchMin : randomPitchMax;
				lowPitch = !lowPitch;
				audioSource.pitch = pitch;
				nextSwitch = AudioSettings.dspTime + GetWaitTime(audioSource.clip, pitch);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}