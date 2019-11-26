using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonScript : MonoBehaviour
{
    public GameObject gameManagerObj;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.EnteredLevel1Balloon();
        }
    }
}
