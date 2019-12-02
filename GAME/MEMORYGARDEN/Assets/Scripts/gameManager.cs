using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Global Resources")]
    public DateTime _systemTime = System.DateTime.Now;
    public GameObject[] charRemains;
    public GameObject level1Transport;
    public Camera level1UnlockCamera;
    public Camera mainCamera;
    public Camera balloonCamera;
    public GameObject player;
    public GameObject level2Spawn;
    public GameObject marioHead;
    public GameObject IntroHall;
    public AudioSource musicSource;
    public AudioClip level1transition;
    public AudioClip rumbling;

    private float timer;
    private float level2timer;
    private bool raiseBalloon = false;
    private bool movingToLevel2 = false;
    private bool level1Complete = false;
    private bool marioDescends = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _systemTime = System.DateTime.Now;
        charRemains = GameObject.FindGameObjectsWithTag("Flower");

        if (raiseBalloon)
        {
            timer += Time.deltaTime;
        }

        if (raiseBalloon && timer >= 10)
        {
            timer = 0;
            raiseBalloon = false;
            level1UnlockCamera.enabled = false;
            mainCamera.enabled = true;
            level1Transport.GetComponent<Collider>().enabled = true;
        }

        if(movingToLevel2)
        {
            level2timer += Time.deltaTime;
        }

        if(movingToLevel2 && level2timer >= 4 && !marioDescends)
        {
            balloonCamera.GetComponent<CameraShake>().enabled = true;
            marioHead.GetComponent<AudioSource>().Play();
            marioHead.GetComponent<Animator>().SetBool("isDescend", true);
            marioDescends = true;
        }

        if (movingToLevel2 && level2timer >= 29)
        {
            movingToLevel2 = false;
            level2timer = 0;
            balloonCamera.enabled = false;
            mainCamera.enabled = true;
            player.transform.position = level2Spawn.transform.position;
            IntroHall.GetComponent<HallUnlockScript>().UnlockLevel2();

        }

        if (charRemains.Length == 3 && !level1Complete)
        {
            raiseBalloon = true;
            //level 1 complete, trigger cutscene of balloon raising up and a door appearing in the hub world
            level1Transport.GetComponent<Animator>().SetBool("raiseBalloon", true);
            mainCamera.enabled = false;
            level1UnlockCamera.enabled = true;
            level1Complete = true;
            level1UnlockCamera.GetComponent<CameraShake>().enabled = true;
            //play rumbling sound
            musicSource.clip = rumbling;
            musicSource.Play();
        }

    }

    public void EnteredLevel1Balloon()
    {
        level1Transport.GetComponent<Animator>().SetBool("travelToLevelTwo", true);
        movingToLevel2 = true;
        mainCamera.enabled = false;
        balloonCamera.enabled = true;
        //play short song
        musicSource.clip = level1transition;
        musicSource.Play();
    }
}
