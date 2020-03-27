using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip selectionSound;
    public AudioClip changeSelectionSound;
    public GameObject selectionCube;

    private float timer;
    private bool selectionMade = false;
    private Animator cubeAnimator;
    private int currentState = 0;

    void Start()
    {
        cubeAnimator = selectionCube.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            soundSource.clip = changeSelectionSound;
            soundSource.Play();

            if(currentState == 0)
            {
                cubeAnimator.SetBool("PlaySelected", false);
                cubeAnimator.SetBool("CreditsSelected", true);
                currentState++;
            }
            else if(currentState == 1)
            {
                cubeAnimator.SetBool("CreditsSelected", false);
                cubeAnimator.SetBool("ExitSelected", true);
                currentState++;
            }
            else if(currentState == 2)
            {
                cubeAnimator.SetBool("ExitSelected", false);
                cubeAnimator.SetBool("PlaySelected", true);
                currentState = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            soundSource.clip = changeSelectionSound;
            soundSource.Play();

            if (currentState == 0)
            {
                cubeAnimator.SetBool("PlaySelected", false);
                cubeAnimator.SetBool("ExitSelected", true);
                currentState = 2;
            }
            else if (currentState == 1)
            {
                cubeAnimator.SetBool("CreditsSelected", false);
                cubeAnimator.SetBool("PlaySelected", true);
                currentState--;
            }
            else if (currentState == 2)
            {
                cubeAnimator.SetBool("ExitSelected", false);
                cubeAnimator.SetBool("CreditsSelected", true);
                currentState--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            soundSource.clip = selectionSound;
            soundSource.Play();

            selectionMade = true;
            cubeAnimator.SetBool("ExitSelected", false);
            cubeAnimator.SetBool("CreditsSelected", false);
            cubeAnimator.SetBool("PlaySelected", false);
            cubeAnimator.SetBool("SelectionMade", true);
        }

        //wait for the sound effect and animation to finish
        if (selectionMade)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 3)
        {
            if (currentState == 0)
                SceneManager.LoadScene("MEMORYLOSS");
            else if (currentState == 1)
                SceneManager.LoadScene("Credits");
            else
                Application.Quit();
        }
    }
}
