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
        videoPlayer.loopPointReached += VideoEnded;
    }

    void Update()
    {
        if (Input.anyKey && !selectionMade)
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
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.Stop();
    }

}
