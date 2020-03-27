using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip selectionSound;

    private float timer;
    private bool selectionMade = false;

    void Update()
    {
        if (Input.anyKey && !selectionMade)
        {
            soundSource.clip = selectionSound;
            soundSource.Play();
            selectionMade = true;

            //change text animation for press start
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
}
