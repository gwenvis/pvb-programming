using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideoPlayer : MonoBehaviour
{
    [SerializeField] RawImage imageVideo;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        yield return new WaitForSeconds(2f);
        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(1f);
            break;
        }
        imageVideo.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
