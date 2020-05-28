using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideoPlayer : MonoBehaviour
{
    VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(2f);
        videoPlayer.Play();
    }
}
