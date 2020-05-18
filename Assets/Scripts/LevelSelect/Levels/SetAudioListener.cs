using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// This is where you can disable and enable the audiolistener of the level select scene.
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
