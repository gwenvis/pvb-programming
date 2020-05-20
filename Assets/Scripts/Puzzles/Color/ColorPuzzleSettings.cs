using System.Linq;
using DN.Service;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// This object holds all settings that are used within the color puzzle
	/// </summary>
	[Service]
	public partial class ColorPuzzleSettings : ScriptableObject
	{
		public float PlayerSpeed => playerSpeed;
		public float DestinationTimeout => destinationTimeout;
		public ColorLineSettings[] ColorSettings => colorSettings;

		[SerializeField, Tooltip("speed per second for the player to move")] 
		private float playerSpeed = 10.0f;

		[SerializeField, Tooltip("Timeout to have when it reaches each destination to continue to the next.")]
		private float destinationTimeout = 0.5f;

		[SerializeField]
		private ColorLineSettings[] colorSettings;

		public UnityEngine.Color GetLineColor(LineColor color)
		{
			return colorSettings.FirstOrDefault(x => x.LineColor == color).Color;
		}
	}
}
