using UnityEngine;

namespace DN.Levelselect.LevelData
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class LevelDataMap : MonoBehaviour
	{
		public enum Level { none, level_1, level_2, level_3};

		public Level level = Level.none;
	}
}
