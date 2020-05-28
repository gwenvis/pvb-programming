using System;
using DN.UI;
using UnityEngine;

namespace DN
{
	/// <summary>
	/// Audio for the draggableNode
	/// </summary>
	public class DraggableNodeAudio : MonoBehaviour
	{
		[SerializeField] private AudioClip dropClip;
		[SerializeField] private AudioClip pickClip;
		[SerializeField] private float volume = 0.6f;
		private AudioSource audioSource;
		private DraggableItem item;

		private void Awake()
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.loop = false;
			audioSource.volume = volume;
			item = GetComponent<DraggableItem>();
			
			item.PickedUpItemEvent += OnPickedUpItemEvent;
			item.DroppedItemEvent += OnDroppedItemEvent;
		}

		private void OnPickedUpItemEvent(DraggableItem obj)
		{
			audioSource.clip = pickClip;
			audioSource.Play();
		}

		private void OnDroppedItemEvent(DraggableItem obj)
		{
			audioSource.clip = dropClip;
			audioSource.Play();
		}
	}
}
