using DN.LevelSelect.SceneManagment;
using DN.Service;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	[Service]
	public class LevelMemoryService
	{
		public BiomeController BiomeController => biomeController;
		public LevelLoader LevelLoader => levelLoader;
		public SetAudioListener SetAudioListener => setAudioListener;
		public DN.LevelData LevelData { get; set; }
		
		private SetAudioListener setAudioListener;
		private LevelLoader levelLoader;
		private BiomeController biomeController;


		public void SetBiomeAndLevelAndAudioController(BiomeController controller, LevelLoader loader, SetAudioListener listener)
		{
			biomeController = controller;
			levelLoader = loader;
			setAudioListener = listener;
		}
	}
}
