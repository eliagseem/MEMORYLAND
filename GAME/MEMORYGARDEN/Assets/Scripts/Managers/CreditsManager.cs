using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 35 || Input.GetKey("escape"))
        {
            //load in second scene
            SceneManager.LoadScene("Main Menu");
        }
    }
}
