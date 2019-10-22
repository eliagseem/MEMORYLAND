using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcastScript : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (objectHit.gameObject.tag == "NPC")
                {
                    //clicked on NPC
                    //make NPC face player
                    //play their dialogue audio clip
                    var agent = objectHit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    agent.isStopped = true;

                }
                else if (objectHit.gameObject.tag == "Door")
                {
                    this.transform.position = new Vector3(-122.5786f, 5, 0);
                }
            }
        }
    }
}
