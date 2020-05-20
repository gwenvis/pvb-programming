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
		private LineDrawing lineDrawMode;
		private LevelEditorColorData editorData;
		private bool loaded = false;
		private bool unsaved = false;

		protected void Awake()
		{
			puzzleEditorView.ItemSelectedEvent += OnItemSelectedEvent;
			puzzleEditorView.ItemDeletedEvent += OnItemDeletedEvent;
			lineDrawMode = GetComponent<LineDrawing>();
		}

		protected void OnDestroy()
		{
			puzzleEditorView.ItemSelectedEvent -= OnItemSelectedEvent;
			puzzleEditorView.ItemDeletedEvent -= OnItemDeletedEvent;
			OnItemSelectedEvent(null);
		}
		
		private void OnItemDeletedEvent(Object obj)
		{
			var monoBehaviour = (MonoBehaviour) obj;
			var line = monoBehaviour.GetComponent<Line>();
			var node = monoBehaviour.GetComponent<Node>();

			if (line)
				DeleteLine(line.Data);
			else if (node)
				DeleteNode(node.Data);
		}
		
		private void OnItemSelectedEvent(Object obj)
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

			foreach (var line in editorData.Lines)
			{
				puzzleEditorView.InstantiateLine(line, editorData.Nodes);
			}

			loaded = true;
			unsaved = true;
			return (true, colorLevelData);
		}

		public bool Save()
		{
			bool saved = loadedData.SetData(this, editorData.Nodes, editorData.Lines, puzzleEditorView.Size);
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

		public void CreateLine(NodeData a, NodeData b, IEnumerable<NodeData> nodes)
		{
			var line = editorData.CreateLine(a.Id, b.Id);
			puzzleEditorView.InstantiateLine(line, nodes);
		}

		public void EnterLineDrawMode()
		{
			lineDrawMode.EnterLineDrawMode(TryCreateLine);
		}

		public void TryCreateLine(Vector3 a, Vector3 b)
		{
			var startNode = puzzleEditorView.GetNodeAtPosition(a);
			var endNode = puzzleEditorView.GetNodeAtPosition(b);

			if (startNode == null || endNode == null) return;

			CreateLine(startNode.Data, endNode.Data, editorData.Nodes);
		}

		private void DeleteNode(NodeData node)
		{
			// first remove all lines connected to this.
			foreach (LineData line in node.ConnectedLines)
			{
				DeleteLine(line);
			}
			
			editorData.RemoveNode(node);
			puzzleEditorView.DeleteNode(node);
		}

		private void DeleteLine(LineData data)
		{
			editorData.RemoveLine(data);
			puzzleEditorView.DeleteLine(data);
		}
	}
}
