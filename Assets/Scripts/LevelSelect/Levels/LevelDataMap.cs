using UnityEngine;

namespace DN.LevelSelect.LevelData
{
	/// <summary>
	/// this is wjere you store the Level that the object chose in here so i can ask for it through the other scripts
	/// </summary>
	public class LevelDataMap : MonoBehaviour
	{
		public enum Level { None = 0, Level1 = 1, Level2 = 2, Level3 = 3
		};

		public Level level = Level.None;
	}
}
