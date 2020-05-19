using System;
using System.Collections.Generic;
using DN.Puzzle.Color.Editor;
using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class PuzzleEditorView : MonoBehaviour
	{
		public event Action<UnityEngine.Object> ItemSelected;
		
		[SerializeField] private Transform viewTransform;
		[SerializeField] private GameObject editorNodePrefab;
		[SerializeField] private GameObject editorLinePrefab;
		[SerializeField] private GameObject editorLineDrawPrefab;

		private Dictionary<NodeData, GameObject> instantiatedNodes = new Dictionary<NodeData, GameObject>();
		private Dictionary<LineData, GameObject> instantiatedLines = new Dictionary<LineData, GameObject>();

		private UnityEngine.Object selectedObject;

		public void InstantiateNode(NodeData data)
		{
			var node = Instantiate(editorNodePrefab, viewTransform);
			node.transform.localPosition = data.Position;

			node.GetComponent<DraggableItem>().DroppedItemEvent += OnDroppedNodeEvent;
			node.AddComponent<ClickableItem>().ClickedEvent += OnNodeClickedEvent;
			node.GetComponent<Node>().InitializeNode(data);

			instantiatedNodes.Add(data, node);
		}

		private void OnNodeClickedEvent(ClickableItem item)
		{
			if (item.Equals(selectedObject))
			{
				SetHighlighOfCurrentObject(false);
				SetSelectedObject(null);
				return;
			}

			if (selectedObject)
			{
				SetHighlighOfCurrentObject(false);
			}
			SetSelectedObject(item);
			SetHighlighOfCurrentObject(true);
		}

		private void SetSelectedObject(UnityEngine.Object obj)
		{
			selectedObject = obj;
			ItemSelected?.Invoke(obj);
		}

		private void SetHighlighOfCurrentObject(bool active) => ((MonoBehaviour) selectedObject).GetComponent<Highlight>().SetHighlight(active);

		private void OnDroppedNodeEvent(DraggableItem obj)
		{
			obj.GetComponent<Node>().Data.SetPosition(obj.transform.localPosition);

			foreach (var line in instantiatedLines)
			{
				line.Value.GetComponent<Line>().InitializePosition();
			}
		}

		public void DeleteNode(NodeData data)
		{
			instantiatedNodes[data].GetComponent<DraggableItem>().DroppedItemEvent -= OnDroppedNodeEvent;
			Destroy(instantiatedNodes[data]);
			instantiatedNodes.Remove(data);
		}

		public void InstantiateLine(LineData data, IEnumerable<NodeData> nodes)
		{
			var lineObj = Instantiate(editorLinePrefab, viewTransform);
			var line = lineObj.GetComponent<Line>();
			
			line.InitializeData(data);
			line.Data.SetOwner(line, nodes);
			line.AssignToParent();
			
			lineObj.AddComponent<ClickableItem>().ClickedEvent += OnNodeClickedEvent;

			instantiatedLines.Add(data, lineObj);
		}
		
		public void DeleteLine(LineData line)
		{
			Destroy(instantiatedLines[line]);
			instantiatedLines.Remove(line);
		}

		public void ClearAll()
		{
			foreach (var nodeKV in instantiatedNodes)
			{
				DeleteNode(nodeKV.Key);
			}

			foreach (var line in instantiatedLines)
			{
				DeleteLine(line.Key);
			}
		}

		public Node GetNodeAtPosition(Vector3 pos)
		{
			var casts = Physics2D.CircleCastAll(pos, 10, Vector2.right);
			foreach (var cast in casts)
			{
				if (cast.collider.GetComponent<Node>())
					return cast.collider.GetComponent<Node>();
			}

			return null;
		}
	}
}
