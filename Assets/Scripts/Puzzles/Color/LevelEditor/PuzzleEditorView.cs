using System.Collections.Generic;
using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class PuzzleEditorView : MonoBehaviour
	{
		[SerializeField] private Transform viewTransform;
		[SerializeField] private GameObject editorNodePrefab;
		[SerializeField] private GameObject editorLinePrefab;

		private Dictionary<NodeData, GameObject> instantiatedNodes = new Dictionary<NodeData, GameObject>();
		private Dictionary<LineData, GameObject> instantiatedLines = new Dictionary<LineData, GameObject>();
		
		public void InstantiateNode(NodeData data)
		{
			var node = Instantiate(editorNodePrefab, viewTransform);
			node.transform.localPosition = data.Position;

			node.GetComponent<DraggableItem>().DroppedItemEvent += OnDroppedNodeEvent;
			node.GetComponent<Node>().InitializeNode(data);
			
			instantiatedNodes.Add(data, node);
		}

		private void OnDroppedNodeEvent(DraggableItem obj)
		{
			obj.GetComponent<Node>().Data.SetPosition(obj.transform.localPosition);
		}

		public void DeleteNode(NodeData data)
		{
			instantiatedNodes[data].GetComponent<DraggableItem>().DroppedItemEvent -= OnDroppedNodeEvent;
			Destroy(instantiatedNodes[data]);
			instantiatedNodes.Remove(data);
		}
		
		public void InstantiateLine(LineData data)
		{
			
		}
		
		public void DeleteLine(LineData line)
		{
			
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
	}
}
