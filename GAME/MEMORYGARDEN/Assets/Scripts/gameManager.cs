using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [Header("Global Resources")]
    public DateTime _systemTime = System.DateTime.Now;
    public GameObject[] charRemains;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _systemTime = System.DateTime.Now;
        charRemains = GameObject.FindGameObjectsWithTag("Flower");
        if(charRemains.Length == 4)
        {
            //level 1 complete, trigger cutscene of balloon raising up and a door appearing in the hub world
            Debug.Log("level complete");
        }
    }
}
