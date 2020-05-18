using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class SetAudioListener : MonoBehaviour
	{
		[SerializeField] private AudioListener audioListener;

		public void SetListener(bool value)
		{
			audioListener.enabled = value;
		}
	}
}
