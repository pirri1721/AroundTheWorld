using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        //StartCoroutine(PlayVideo());
        videoPlayer.Prepare();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWastedCoroutine();
        }
    }

    public void StartWastedCoroutine()
    {
        StartCoroutine(PlayVideo());
    }
	
	public IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
}
