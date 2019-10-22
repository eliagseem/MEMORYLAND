using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        if(collision.gameObject.tag == "Player")
        {
            transform.position = new Vector3(-122.5786f, 0, 0);
        }
    }
}
