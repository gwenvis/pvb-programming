using System;
using System.Collections.Generic;
using System.Linq;
using DN.Service;
using TMPro;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Gets all levels that are available and 
	/// </summary>
	public class LevelDataDropdown : MonoBehaviour
	{
		public ColorLevelData SelectedLevelData => levelData.ElementAtOrDefault(dropdown.value);
		
		[SerializeField] private TMP_Dropdown dropdown;

		private List<ColorLevelData> levelData;
		
		protected void Start()
		{
			RefreshDropdown();
		}

		public void Refresh() => RefreshDropdown();

		private void RefreshDropdown()
		{
			dropdown.ClearOptions();
			var data = Resources.LoadAll<ColorLevelData>("");
			if (data.Length == 0) return;

			levelData = new List<ColorLevelData>(data);
			dropdown.AddOptions(data.Select(x => x.LevelName).ToList());
		}

		protected void OnEnable()
		{
			dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
		}

		private void OnDropdownValueChanged(int arg0)
		{
			
		}
	}
}
