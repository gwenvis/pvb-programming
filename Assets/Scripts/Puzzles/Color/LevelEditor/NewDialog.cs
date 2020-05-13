using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DN.Puzzle.Color;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Color.Puzzle.Editor
{
	/// <summary>
	/// The popup dialog for the color puzzle that allows the designer to
	/// name the level before creating it.
	/// </summary>
	public class NewDialog : MonoBehaviour
	{
		[SerializeField] private TMP_InputField inputField;
		[SerializeField] private TextMeshProUGUI errorMessageText;
		[SerializeField] private Button okButton;
		[SerializeField] private Button cancelButton;

		private Coroutine coroutine;
		public Action<ColorLevelData> callback;

		protected void OnEnable()
		{
			okButton.onClick.AddListener(OnOkButtonClicked);
			cancelButton.onClick.AddListener(OnCancelButtonClicked);
		}
		
		protected void OnDisable()
		{
			okButton.onClick.RemoveListener(OnOkButtonClicked);
			cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
		}

		public void SetCallback(Action<ColorLevelData> colorLevelData)
		{
			callback = colorLevelData;
		}
		
		private void OnCancelButtonClicked()
		{
			Destroy(gameObject);
		}

		private void OnOkButtonClicked()
		{
			if (string.IsNullOrWhiteSpace(inputField.text))
			{
				ShowError("A level name is required before creating it.");
				return;
			}

			// check if the puzzle name already exists
			ColorLevelData[] levels = Resources.FindObjectsOfTypeAll<ColorLevelData>();

			if (levels.Any(x => x.LevelName.Equals(inputField.text)))
			{
				ShowError("This level name already exists");
				return;
			}

			ColorLevelData levelData = ScriptableObject.CreateInstance<ColorLevelData>();
			levelData.SetLevelName(inputField.text);
			AssetDatabase.CreateAsset(levelData, $"Assets/Data/Resources/c{levelData.LevelName}.asset");
			AssetDatabase.SaveAssets();

			callback?.Invoke(levelData);
			
			Destroy(gameObject);
		}

		private void ShowError(string message, float time = 5)
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			
			errorMessageText.text = message;
			StartCoroutine(ClearTextDelay(time));
		}

		private IEnumerator ClearTextDelay(float time)
		{
			yield return new WaitForSeconds(time);
			errorMessageText.text = string.Empty;
			coroutine = null;
		}
	}
}
