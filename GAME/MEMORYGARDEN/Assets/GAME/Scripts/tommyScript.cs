using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tommyScript : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            anim.SetTrigger("isMagic");
        }
        else
        {
            anim.SetBool("isIdle", true);
        }
    }
}