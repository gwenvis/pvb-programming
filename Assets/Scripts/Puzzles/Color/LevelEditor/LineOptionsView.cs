#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace DN.Puzzle.Color.Editor
{
	/// <summary>
	/// Options for the lines when selecting them in the editor
	/// </summary>
	public class LineOptionsView : MonoBehaviour
	{
		[SerializeField] private TMP_Dropdown colorDropdown;
		[SerializeField] private Toggle canTraverseBothWaysToggle;

		private PuzzleEditor puzzleEditor;
		private LineData lineData;
		private Line line;

		protected void Awake()
		{
			colorDropdown.ClearOptions();

			foreach (LineColor enumValue in Enum.GetValues(typeof(LineColor)))
			{
				colorDropdown.options.Add(new TMP_Dropdown.OptionData() { text = enumValue.ToString()});
			}

			colorDropdown.onValueChanged.AddListener(ColorDropdownValueChanged);
			canTraverseBothWaysToggle.onValueChanged.AddListener(OnCanTraverseChanged);

			puzzleEditor = GetComponent<PuzzleEditor>();
			puzzleEditor.ItemSelected += NewItemSelectedEvent;
		}

		private void NewItemSelectedEvent(Object obj)
		{
			if (obj == null)
			{
				lineData = null;
				return;
			}

			var objLine = ((MonoBehaviour) obj).GetComponent<Line>();
			if (objLine)
			{
				line = objLine;
				lineData = line.Data;
				
				colorDropdown.SetValueWithoutNotify((int)lineData.LineColor);
				canTraverseBothWaysToggle.SetIsOnWithoutNotify(lineData.CanTraverseBothWays);
				
				return;
			}

			line = null;
			lineData = null;
		}

		private void OnCanTraverseChanged(bool can)
		{
			lineData.SetTraverseBothWays(can);
			line.UpdateLineSprite();
		}

		protected void OnDestroy()
		{
			colorDropdown.onValueChanged.RemoveListener(ColorDropdownValueChanged);
			canTraverseBothWaysToggle.onValueChanged.RemoveListener(OnCanTraverseChanged);
		}

		private void ColorDropdownValueChanged(int value)
		{
			lineData.SetColor((LineColor)value);
			line.UpdateLineSprite();
		}
	}
}
#endif
