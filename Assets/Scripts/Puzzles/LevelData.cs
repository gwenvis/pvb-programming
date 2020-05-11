using UnityEngine;

namespace DN
{
	/// <summary>
	/// ye
	/// </summary>
	public abstract class LevelData : ScriptableObject
	{
		public string LevelName => levelName;
		
		private string levelName;
	}
}
