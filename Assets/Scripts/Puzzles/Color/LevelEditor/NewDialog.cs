using System;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Color.Puzzle.Editor
{
	/// <summary>
	/// The popup dialog for the color puzzle that allows the designer to
	/// name the level before creating it.
	/// </summary>
	public class NewDialog : MonoBehaviour
	{
		[SerializeField] private TMPro.TMP_InputField inputField;
		[SerializeField] private Button okButton;
		[SerializeField] private Button cancelButton;

		protected void OnEnable()
		{
			okButton.onClick.AddListener(OnOkButtonClicked);
			cancelButton.onClick.AddListener(OnCancelButtonClicked);
		}
		
		protected void OnDisable()
		{
			okButton.onClick.RemoveListener(OnOkButtonClicked);
			cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
		}
		
		private void OnCancelButtonClicked()
		{
			throw new NotImplementedException();
		}

		private void OnOkButtonClicked()
		{
			throw new NotImplementedException();
		}
	}
}
