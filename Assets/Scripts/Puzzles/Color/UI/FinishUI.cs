using DN.Puzzle.Color;
using UnityEngine;
using UnityEngine.UI;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class FinishUI : MonoBehaviour
	{
		[SerializeField] private Transform stuckText;
		[SerializeField] private Transform finishedText;
		[SerializeField] private Transform unfinishedText;

		private Transform textTransform;

		private void OnEnable()
		{
			Player.RunFinishedEvent += OnRunFinishedEvent;
		}

		private void OnDisable()
		{
			Player.RunFinishedEvent -= OnRunFinishedEvent;
		}

		private void Update()
		{
			if(textTransform)
				textTransform.localScale = Vector3.Lerp(textTransform.localScale, Vector3.one, 15 * Time.deltaTime);
		}

		private void OnRunFinishedEvent(Player.State endState)
		{
			if(endState == Player.State.Stuck)
			{
				textTransform = stuckText.transform;
			}
			else if(endState == Player.State.Navigating)
			{
				textTransform = unfinishedText.transform;
			}
			else if(endState == Player.State.Done)
			{
				textTransform = finishedText.transform;
			}

			textTransform.gameObject.SetActive(true);
			textTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
	}
}
