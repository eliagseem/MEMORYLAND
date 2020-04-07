using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip selectionSound;
    public GameObject flashingText;
    public UnityEngine.Video.VideoPlayer videoPlayer;

    private float timer;
    private bool selectionMade = false;

    void Start()
    {
        //if it's the webgl build, assign url to clip because
        //unity doesn't support asset clips playing... big brane buckos
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "memland_intro_streaming.mp4");

        videoPlayer.Play();
        videoPlayer.loopPointReached += VideoEnded;
    }

    void Update()
    {
        //wait for the sound effect and animation to finish
        if (selectionMade)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 6)
        {
            //load in second scene
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        else if(Input.anyKeyDown && !selectionMade)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
            }
            else if(!videoPlayer.isPlaying)
            {
                soundSource.clip = selectionSound;
                soundSource.Play();
                selectionMade = true;

                //flash text faster to indicate selection
                flashingText.GetComponent<FlashingTextScript>().flashTimer = .1f;
            }
        }
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.Stop();
    }

}
