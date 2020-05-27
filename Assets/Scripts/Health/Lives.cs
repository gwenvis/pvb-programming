using DN.Service;
using System;
using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// Takes care of the lives the player has to be added to Canvas.
	/// </summary>
	public class Lives : MonoBehaviour
	{
		[SerializeField] private float heartSize = 50f;
		public event Action<int> LifeLostEvent;
		public event Action AllLifeLost;
		LivesUI livesUI;

		public int CurrentLives { get; private set; }

		private int startLives = 3;

		private void Start()
		{
			livesUI = new LivesUI(gameObject, this, heartSize);
		}

		public Lives()
		{
			if (!ServiceLocator.Locate<LivesService>().RunOnce)
			{
				ServiceLocator.Locate<LivesService>().SetCurrentLives(startLives, true);
				CurrentLives = ServiceLocator.Locate<LivesService>().CurrentLives;
			}
			else
			{
				CurrentLives = ServiceLocator.Locate<LivesService>().CurrentLives;
			}
		}

		public void LoseLife()
		{	
			CurrentLives--;
			LifeLostEvent?.Invoke(CurrentLives);

			if (CurrentLives <= 0)
			{
				AllLifeLost?.Invoke();
			}
		}
	}
}
