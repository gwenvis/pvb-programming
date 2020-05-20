using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Tutorial
{
	/// <summary>
	/// This is where the typing animation happens.
	/// </summary>
	public class TextWriterSingle : MonoBehaviour
	{
		private Text uiText;
		private string textToWrite;
		private int characterIndex;
		private float timePerCharacter;
		private float timer;
		private bool invisibleCharacters;

		public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
		{
			this.uiText = uiText;
			this.textToWrite = textToWrite;
			this.timePerCharacter = timePerCharacter;
			this.invisibleCharacters = invisibleCharacters;
			characterIndex = 0;
		}

		public bool Animate()
		{
			timer -= Time.deltaTime;
			while (timer <= 0f)
			{
				timer += timePerCharacter;
				characterIndex++;
				string text = textToWrite.Substring(0, characterIndex);
				if (invisibleCharacters)
				{
					text += "<color=#2E2F2E>" + textToWrite.Substring(characterIndex) + "</color>";
				}

				uiText.text = text;

				if (characterIndex >= textToWrite.Length)
				{
					return true;
				}
			}
			return false;
		}
	}
}
