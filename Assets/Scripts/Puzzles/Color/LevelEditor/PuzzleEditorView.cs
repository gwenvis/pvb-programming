using System;
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
		public event Action<UnityEngine.Object> ItemSelected;
		
		[SerializeField] private Transform viewTransform;
		[SerializeField] private GameObject editorNodePrefab;
		[SerializeField] private GameObject editorLinePrefab;
		[SerializeField] private GameObject editorLineDrawPrefab;

		private Dictionary<NodeData, GameObject> instantiatedNodes = new Dictionary<NodeData, GameObject>();
		private Dictionary<LineData, GameObject> instantiatedLines = new Dictionary<LineData, GameObject>();

		private UnityEngine.Object selectedObject;

		private bool inLineMode = false;
		private GameObject editorLineDrawObject;

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
				SetSelectedObject(null);
				return;
			}

			SetSelectedObject(item);
		}

		private void SetSelectedObject(UnityEngine.Object obj)
		{
			// TODO : Do something with this item. (like adding a background or changing the color)
			selectedObject = obj;
			ItemSelected?.Invoke(obj);
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

		public void EnterLineCreationMode()
		{
			if (inLineMode)
			{
				
			}
			
			inLineMode = true;
		}

		public void ExitLineMode()
		{
			
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
