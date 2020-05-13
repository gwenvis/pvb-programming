using UnityEngine;

namespace DN
{
	/// <summary>
	/// ye
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
