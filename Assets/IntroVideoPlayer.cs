using DN.LevelSelect.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DN.Intro
{
    /// <summary>
    /// Here the Intro video is played and handles the stuff after it is done playing
    /// </summary>
    public class IntroVideoPlayer : MonoBehaviour
    {
        [SerializeField] private RawImage imageVideo;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private string videoPath;

        private VideoPlayer videoPlayer;

        private void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                videoPlayer.url = videoPath;
            StartCoroutine(PlayVideo());
            videoPlayer.loopPointReached += Endreached;
        }

        private void Endreached(UnityEngine.Video.VideoPlayer vp)
        {
            StartCoroutine(levelLoader.LoadLevelSelect());
        }

        private IEnumerator PlayVideo()
        {
            videoPlayer.Prepare();
            yield return new WaitForSeconds(1f);
            while (!videoPlayer.isPrepared)
            {
                yield return new WaitForSeconds(1f);
                break;
            }
            imageVideo.texture = videoPlayer.texture;
            videoPlayer.Play();
        }
    }
}
