using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DN.Tutorial
{
	/// <summary>
	/// Here
	/// </summary>
	public class PlayVideo : MonoBehaviour
	{
		[SerializeField] private RawImage rawImage;
		[SerializeField] private VideoPlayer videoPlayer;

		public void StartVideo()
		{
			StartCoroutine(PlayInstruction());
		}

		public IEnumerator PlayInstruction()
		{ 
			yield return new WaitForSeconds(0);
			Debug.Log("Play");
			rawImage.texture = videoPlayer.texture;
			videoPlayer.Play();
		}
	}
}
