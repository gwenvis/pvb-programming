using System;
using DN.Color.Puzzle.Editor;
using DN.Puzzle.Color;
using DN.Puzzle.Color.Editor;
using DN.Service;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Handles saving and loading the data when the user clicks the button.
	///
	/// Note: not actually responsible for saving the data itself yet, just for calling the proper events
	/// when the user clicks on the buttons.
	/// </summary>
	public class DataManager : MonoBehaviour
	{
		[SerializeField] private Button loadButton;
		[SerializeField] private Button saveButton;
		[SerializeField] private Button createNewButton;
		[SerializeField] private TMPro.TextMeshProUGUI dataLabels;
		[SerializeField] private LevelDataDropdown levelDataDropdown;
		[SerializeField] private PuzzleEditor puzzleEditor;

		private EditorPrefabsData editorPrefabsData;
		private ColorLevelData loadedLevelData;

		protected void Awake()
		{
			editorPrefabsData = ServiceLocator.Locate<EditorPrefabsData>();
		}

		protected void OnEnable()
		{
			loadButton.onClick.AddListener(OnLoadButtonClicked);
			saveButton.onClick.AddListener(OnSaveButtonClicked);
			createNewButton.onClick.AddListener(OnCreateNewButtonClicked);
		}

		private void OnCreateNewButtonClicked()
		{
			NewDialog dialog = Instantiate(editorPrefabsData.CreateNewPopup);
			dialog.SetCallback(NewFileCreatedCallback);
		}

		private void NewFileCreatedCallback(ColorLevelData obj)
		{
			levelDataDropdown.Refresh();
		}

		private void OnSaveButtonClicked()
		{
			SetText(puzzleEditor.Save() ? "Saving successful!" : "Saving failed...");
		}

		private void OnLoadButtonClicked()
		{
			var (loaded, colorLevelData) = puzzleEditor.Load(levelDataDropdown.SelectedLevelData);
			if (loaded)
				LoadingSucceedCallback(colorLevelData);
			else
				LoadingFailedCallback();
		}

		private void LoadingSucceedCallback(ColorLevelData obj)
		{
			loadedLevelData = obj;
			SetText($"Loaded {obj.LevelName}");
		}

		private void LoadingFailedCallback()
		{
			SetText("Loading failed, data would be overwritten.");
		}

		private void SetText(string text)
		{
			dataLabels.text = text;
		}
	}
}
