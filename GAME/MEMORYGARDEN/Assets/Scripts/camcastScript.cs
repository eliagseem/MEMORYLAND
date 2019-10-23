using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcastScript : MonoBehaviour
{
    public Camera camera;
    public Transform guide;
    private bool isHoldingObject = false;
    private GameObject pickedUpObj;

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
                    if (objectHit.gameObject.GetComponent<npcScript>().isWandering)
                    {
                        //play their dialogue audio clip
                        var npc = objectHit.gameObject;
                        var agent = objectHit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                        npc.GetComponent<npcScript>().stopWandering();
                        npc.GetComponent<npcScript>().talkToPlayer();
                    }
                    else
                    {
                        var npc = objectHit.gameObject;
                        npc.GetComponent<npcScript>().startWandering();
                    }
                }
                else if (objectHit.gameObject.tag == "Door")
                {
                    this.transform.position = new Vector3(-122.5786f, 5, 0);
                }
                else if (objectHit.gameObject.tag == "DesiredObject")
                {
                    pickedUpObj = objectHit.gameObject;

                    if (isHoldingObject)
                    {
                        isHoldingObject = false;
                        pickedUpObj.GetComponent<Rigidbody>().useGravity = true;
                        pickedUpObj.transform.SetParent(null);
                        pickedUpObj = null;
                    }
                    else
                    {
                        objectHit.gameObject.transform.SetParent(guide);
                        isHoldingObject = true;
                        objectHit.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
        }

        if (isHoldingObject)
        {
            pickedUpObj.transform.position = guide.position;
        }
    }
}
