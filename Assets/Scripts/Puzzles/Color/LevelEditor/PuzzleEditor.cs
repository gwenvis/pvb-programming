using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Puzzle editor for the color puzzle. This handles loading, saving
	/// and the creation of new objects.
	/// </summary>
	public class PuzzleEditor : MonoBehaviour
	{
		public event Action<Object> ItemSelected;
		
		[SerializeField] private PuzzleEditorView puzzleEditorView;
		[SerializeField] private GameObject lineOptionsView;
		[SerializeField] private GameObject nodeOptionsView;
		private ColorLevelData loadedData;
		private LevelEditorColorData editorData;
		private bool loaded = false;
		private bool unsaved = false;

		private bool inLineDrawMode;

		protected void Awake()
		{
			puzzleEditorView.ItemSelected += OnItemSelected;
		}

		protected void OnDestroy()
		{
			puzzleEditorView.ItemSelected -= OnItemSelected;
			OnItemSelected(null);
		}

		private void OnItemSelected(Object obj)
		{
			ItemSelected?.Invoke(obj);
			
			lineOptionsView.SetActive(false);
			nodeOptionsView.SetActive(false);

			if (obj == null)
				return;
			
			var mono = (MonoBehaviour) obj;

			unsaved = true;

			if (mono.GetComponent<Line>())
				lineOptionsView.SetActive(true);
			else if (mono.GetComponent<Node>())
				nodeOptionsView.SetActive(true);
		}

		public (bool, ColorLevelData) Load(ColorLevelData colorLevelData) 
		{
			if (loaded || unsaved)
			{
				return (false, null);
			}

			loadedData = colorLevelData;
			editorData = new LevelEditorColorData(loadedData.Nodes, loadedData.Lines);

			foreach (var node in editorData.Nodes)
			{
				puzzleEditorView.InstantiateNode(node);
			}

			loaded = true;
			unsaved = true;
			return (true, colorLevelData);
		}

		public bool Save()
		{
			bool saved = loadedData.SetData(this, editorData.Nodes, editorData.Lines);
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(loadedData);
			AssetDatabase.SaveAssets();

			unsaved = false;
			
			return saved;
		}

		public void CreateNode()
		{
			var node = editorData.CreateNode();
			puzzleEditorView.InstantiateNode(node);
		}

		public void EnterLineDrawMode()
		{
			inLineDrawMode = true;
		}
	}
}
