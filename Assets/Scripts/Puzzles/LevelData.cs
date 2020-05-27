using UnityEngine;

namespace DN
{
	/// <summary>
	/// base level data that should be loaded by the levels and can be inserted in the inspector
	/// </summary>
	public abstract class LevelData : ScriptableObject
	{
		public string LevelName => levelName;

		[SerializeField] private string levelName;

		public void SetLevelName(string newName)
		{
			if (string.IsNullOrWhiteSpace(levelName))
			{
				levelName = newName;
			}
		}
	}
}
