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

		private LevelLoader levelLoader;
		private BiomeController biomeController;

		public void SetBiomeAndLevelController(BiomeController controller, LevelLoader loader)
		{
			biomeController = controller;
			levelLoader = loader;
		}
	}
}
