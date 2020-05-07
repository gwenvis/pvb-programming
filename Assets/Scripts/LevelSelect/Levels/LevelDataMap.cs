using UnityEngine;

namespace DN.Levelselect.LevelData
{
	/// <summary>
	/// I store the Level that the object chose in here so i can ask for it through the other scripts
	/// </summary>
	public class LevelDataMap : MonoBehaviour
	{
		public enum Level { none, level_1, level_2, level_3};

		public Level level = Level.none;
	}
}
