using DN.Service;
using UnityEngine;

namespace DN.Music
{
	[Service]
	public class SongData : ScriptableObject
	{
		public Song[] Songs => songs;
		public bool RunOnStart => runOnStart;
		public string SceneToLoad => sceneToLoad;
		
		[SerializeField] private Song[] songs;
		[SerializeField] private bool runOnStart;
		[SerializeField] private string sceneToLoad;
	}
}