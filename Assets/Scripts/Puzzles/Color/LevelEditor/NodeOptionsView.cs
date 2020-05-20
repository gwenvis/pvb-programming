using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Customizable options for the selected Node.
	/// </summary>
	public class NodeOptionsView : MonoBehaviour
	{
		[SerializeField] private Toggle isFinishToggle;
		[SerializeField] private Toggle isStartToggle;
		
		private PuzzleEditor puzzleEditor;
		private NodeData selectedNode;

		protected void Awake()
		{
			puzzleEditor = GetComponent<PuzzleEditor>();
			puzzleEditor.ItemSelected += OnItemSelected;
			
			isFinishToggle.onValueChanged.AddListener(OnIsFinishValueChanged);
			isStartToggle.onValueChanged.AddListener(OnStartValueChanged);
		}

		private void OnIsFinishValueChanged(bool value)
		{
			if(isStartToggle.isOn && value == true)
				isStartToggle.SetIsOnWithoutNotify(false);
			Apply();
		}
		
		private void OnStartValueChanged(bool value)
		{
			if(isFinishToggle.isOn && value == true)
				isFinishToggle.SetIsOnWithoutNotify(false);
			Apply();
		}

		private void Apply()
		{
			selectedNode.SetIsFinish(isFinishToggle.isOn);
			selectedNode.SetIsStart(isStartToggle.isOn);
		}

		private void OnItemSelected(Object obj)
		{
			if (obj == null)
			{
				selectedNode = null;
				return;
			}
			
			Node node = ((MonoBehaviour) obj).GetComponent<Node>();
			if (!node)
			{
				selectedNode = null;
				return;
			}

			selectedNode = node.Data;

			isFinishToggle.SetIsOnWithoutNotify(selectedNode.IsFinish);
			isStartToggle.SetIsOnWithoutNotify(selectedNode.IsStart);
		}
	}
}
