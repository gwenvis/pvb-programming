using DN.LevelSelect.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DN.Intro
{
    /// <summary>
    /// Here the Intro video is played and handles the stuff after it is done playing
    /// </summary>
    public class EndVideoController : MonoBehaviour
    {
        [SerializeField] private RawImage imageVideo;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private Animator animalsAnim;

        private VideoPlayer videoPlayer;

        private void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            StartCoroutine(PlayIntroVideo());
            videoPlayer.loopPointReached += Endreached;
        }

        private void Endreached(UnityEngine.Video.VideoPlayer vp)
        {
            StartCoroutine(StartEndTransition());
        }

        private IEnumerator StartEndTransition()
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(levelLoader.StartEndTransition());
        }

        private IEnumerator PlayIntroVideo()
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
