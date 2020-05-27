using System;
using System.Collections;
using System.Linq;
using DN.Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DN.Music
{
	/// <summary>
	/// Handles the playing of music
	/// </summary>
	public class SongPlayer : MonoBehaviour
	{
		public event Action DestroyEvent;

		public enum SourceIndex
		{
			All = 0,
			Some = 1
		}

		[SerializeField, Range(0, 1)] private float volume = 1;

		[SerializeField, Tooltip("-1 for random")]
		private int startingSongIndex = 0;

		[SerializeField] private bool avoidRepetition = true;
		[SerializeField] private GameObject audioSourceTemplateObject;

		[SerializeField, Tooltip("Volume in seconds")]
		private float crossfadeSpeed = 0.089f;

		[SerializeField] private bool startOnAwake = true;

		private Song[] songs;
		private AudioSource[] audioSources = new AudioSource[2];
		private Song currentSong;
		private int lastSong = -1;
		private SourceIndex sourceIndex = SourceIndex.Some;
		private float playTimeLeft = 0.0f;

		private Coroutine crossfadeCoroutine;
		private bool IsPlaying => audioSources.Any(x => x.isPlaying);

		private void Awake()
		{
			// make two audio sources.
			for (int i = 0; i < audioSources.Length; i++)
			{
				GameObject obj = Instantiate(audioSourceTemplateObject, transform);
				obj.name = ((SourceIndex) i).ToString();
				audioSources[i] = obj.GetComponent<AudioSource>();
				obj.SetActive(true);
			}

			ServiceLocator.Locate<SongService>().SetPlayer(this);

			songs = ServiceLocator.Locate<SongData>().Songs;
			SetVolume(volume);
			StartCoroutine(SongQueue());
			if (startOnAwake)
			{
				SetStartingSong();
				Play();
			}
		}

		private void OnDestroy()
		{
			DestroyEvent?.Invoke();
		}

		public void Play()
		{
			bool playingAll = sourceIndex == SourceIndex.All;
			var allSource = audioSources[(int) SourceIndex.All];
			var someSource = audioSources[(int) SourceIndex.Some];
			allSource.volume = playingAll ? volume : 0;
			allSource.clip = currentSong.AllStems;
			someSource.volume = playingAll ? 0 : volume;
			someSource.clip = currentSong.SomeStems;

			foreach (var audioSource in audioSources)
			{
				audioSource.Play();
				playTimeLeft = currentSong.AllStems.length - audioSource.time;
			}
		}

		public void StopSong()
		{
			foreach (var audioSource in audioSources)
			{
				audioSource.Stop();
			}

			StopCoroutine(crossfadeCoroutine);
			playTimeLeft = 0;
		}

		public void CrossFadeTo(SourceIndex sourceIndex)
		{
			if (sourceIndex == this.sourceIndex) return;
			StopCoroutine(crossfadeCoroutine);

			AudioSource from =
				audioSources[(int) (sourceIndex == SourceIndex.All ? SourceIndex.Some : SourceIndex.All)];
			AudioSource to = audioSources[(int) (sourceIndex == SourceIndex.All ? SourceIndex.All : SourceIndex.Some)];
			crossfadeCoroutine = StartCoroutine(Crossfade(from, to));
			this.sourceIndex = sourceIndex;
		}

		private IEnumerator Crossfade(AudioSource from, AudioSource to)
		{
			while (Math.Abs(from.volume) > Mathf.Epsilon)
			{
				from.volume = Mathf.MoveTowards(from.volume, 0, crossfadeSpeed * Time.deltaTime);
				to.volume = Mathf.MoveTowards(to.volume, volume, crossfadeSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}

			// for good measure (for example if "from" starts on 0)
			from.volume = 0;
			to.volume = volume;
			crossfadeCoroutine = null;
		}

		private void SetStartingSong()
		{
			if (startingSongIndex < 0 || startingSongIndex >= songs.Length)
			{
				currentSong = songs[Random.Range(0, songs.Length)];
				return;
			}

			currentSong = songs[startingSongIndex];
		}

		private void SetNewSong()
		{
			int chosenSong = 0;
			do
			{
				chosenSong = Random.Range(0, songs.Length);
			} while (avoidRepetition && chosenSong == lastSong && songs.Length > 1);

			lastSong = chosenSong;
			currentSong = songs[chosenSong];
		}

		public void Mute(bool mute)
		{
			foreach (var t in audioSources)
			{
				t.mute = mute;
			}
		}

		public void SetVolume(float volume)
		{
			this.volume = volume;
			foreach (var audioSource in audioSources)
			{
				audioSource.volume = volume;
			}
		}

		private new void StopCoroutine(Coroutine coroutine)
		{
			if (coroutine != null) base.StopCoroutine(coroutine);
		}

		private IEnumerator SongQueue()
		{
			// wait until a song starts playing
			while (!IsPlaying)
			{
				yield return new WaitForEndOfFrame();
			}
			
			// song is playing, get the time.
			bool playing = true;
			while (playing)
			{
				float waitTime = audioSources[0].clip.length - audioSources[0].time;
				yield return new WaitForSeconds(waitTime);

				playing = audioSources[0].clip.length - audioSources[0].time > 1.0f && audioSources[0].time > Mathf.Epsilon;
			}
			
			// song has ended, queue the next and restart this coroutine.
			SetNewSong();
			Play();
			StartCoroutine(SongQueue());
		}
	}
}