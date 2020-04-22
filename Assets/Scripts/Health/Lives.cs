using System;

namespace DN.UI
{
	/// <summary>
	/// Takes care of the lives the player has to be added to Canvas.
	/// </summary>
	public class Lives
	{
		public event Action<int> LifeLostEvent;
		public event Action AllLifeLost;

		public int CurrentLives { get; private set; }

		private int startLives = 3;

		public Lives()
		{
			CurrentLives = startLives;
		}

		public void LoseLife()
		{
			CurrentLives--;
			LifeLostEvent?.Invoke(CurrentLives);

			if(CurrentLives <= 0)
			{
				AllLifeLost?.Invoke();
			}
		}
	}
}
