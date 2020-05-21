using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DN.Tutorial
{
	/// <summary>
	/// THis is where you can call the actual animation and her you can see when the animation is done.
	/// </summary>
	public class TextWriter : MonoBehaviour
	{
		private static TextWriter instance;

		private List<TextWriterSingle> textWriterSingles = new List<TextWriterSingle>();

		private float readTime = 2f;

		private GameObject movementKeys;
		private GameObject beginTextObj;
		private VideoPlayer videoPlayer;

		private void Awake()
		{
			instance = this;
		}

		public static void AddWrite_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, GameObject gameObject, GameObject nextDisplay, VideoPlayer playVideo)
		{
			instance.AddWrite(uiText, textToWrite, timePerCharacter, invisibleCharacters, gameObject, nextDisplay, playVideo);
		}

		public void AddWrite(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, GameObject gameObject, GameObject nextDisplay, VideoPlayer playVideo)
		{
			videoPlayer = playVideo;
			movementKeys = gameObject;
			beginTextObj = nextDisplay;
			textWriterSingles.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
		}

		private void Update()
		{
			for (int i = 0; i < textWriterSingles.Count; i++)
			{
				bool destroyInstance = textWriterSingles[i].Animate();
				if (destroyInstance)
				{
					StartCoroutine(SetUIElements());
					textWriterSingles.RemoveAt(i);
					i--;
				}
			}
		}

		private IEnumerator SetUIElements()
		{
			yield return new WaitForSeconds(readTime);
			if (videoPlayer != null)
			{
				videoPlayer.Play();
			}
			movementKeys.SetActive(true);
			beginTextObj.SetActive(false);
		}
	}
}
