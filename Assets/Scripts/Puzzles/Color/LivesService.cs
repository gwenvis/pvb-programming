using DN.Service;
using DN.UI;

namespace DN
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	[Service]
	public class LivesService
	{
		public int CurrenlivesLives => lives;
		public bool RunOnce => runOnce;

		private int lives;

		private bool runOnce;

		public void SetCurrentLives(int currentLives, bool runnedOnce)
		{
			runOnce = runnedOnce;
			lives = currentLives;
		}
	}
}
