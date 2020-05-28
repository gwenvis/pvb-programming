using DN.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DN.Music
{
	/// <summary>
	/// A service that can be called for playing next songs
	/// </summary>
	[Service]
	public class SongService
	{
		private SongPlayer songPlayer;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static void LoadMusicScene()
		{
			var songData = ServiceLocator.Locate<SongData>();
			if (songData.RunOnStart)
			{
				SceneManager.LoadScene(songData.SceneToLoad, LoadSceneMode.Additive);
			}
		}

		public void SetPlayer(SongPlayer player)
		{
			songPlayer = player;
			player.DestroyEvent += DisposePlayer;
		}

		private void DisposePlayer()
		{
			songPlayer = null;
			Debug.Log("Song player has been destroyed.");
		}

		public void EnteringLevelSelect()
		{
			if(songPlayer) songPlayer.CrossFadeTo(SongPlayer.SourceIndex.Some);
		}

		public void ExitingLevelSelect()
		{
			if(songPlayer) songPlayer.CrossFadeTo(SongPlayer.SourceIndex.All);
		}

		public void Mute(bool mute)
		{
			songPlayer.Mute(mute);
		}
	}
}
