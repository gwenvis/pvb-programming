using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Puzzle editor for the color puzzle. This handles loading, saving
	/// and the creation of new objects.
	/// </summary>
	public class PuzzleEditor : MonoBehaviour
	{
		[SerializeField] private PuzzleEditorView puzzleEditorView;
		private ColorLevelData loadedData;
		private LevelEditorColorData editorData;
		private bool loaded;

		private bool inLineDrawMode;
		
		public (bool, ColorLevelData) Load(ColorLevelData colorLevelData) 
		{
			if (loaded)
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
			return (true, colorLevelData);
		}

		public bool Save()
		{
			bool saved = loadedData.SetData(this, editorData.Nodes, editorData.Lines);
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(loadedData);
			AssetDatabase.SaveAssets();

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
