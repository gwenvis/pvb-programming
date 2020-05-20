using DN.Puzzle.Color.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// buttons for creating the objects
	/// </summary>
	public class ObjectCreation : MonoBehaviour
	{
		[SerializeField] private GameObject objectCreationParent;
		[SerializeField] private Button createNodeButton;
		[SerializeField] private Button createLineButton;
		[SerializeField] private PuzzleEditor puzzleEditor;

		protected void OnEnable()
		{
			createNodeButton.onClick.AddListener(OnCreateNodeButtonClicked);
			createLineButton.onClick.AddListener(OnCreateLineButtonClicked);
		}

		protected void OnDisable()
		{
			createNodeButton.onClick.RemoveListener(OnCreateNodeButtonClicked);
			createLineButton.onClick.RemoveListener(OnCreateLineButtonClicked);
		}

		private void OnCreateLineButtonClicked()
		{
			puzzleEditor.EnterLineDrawMode();
		}

		private void OnCreateNodeButtonClicked()
		{
			puzzleEditor.CreateNode();
		}

		public void SetActive(bool active)
		{
			
		}
	}
}
