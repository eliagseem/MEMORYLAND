using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCONTROL : MonoBehaviour {

    public bool isGrounded;
    public bool isCrouching;
    public bool isAttacking; 

    private float speed;
    private float w_speed = 0.05f;
    private float r_speed = 0.1f;
    private float c_speed = 0.025f;
    public float rotSpeed;
    public float jumpHeight;
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        isGrounded = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("isAttacking");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            anim.SetTrigger("isMagic");
        }
        else
        {
            anim.SetBool("isIdle", true);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            //Debug.Log("crouching");
            isCrouching = true;
            anim.SetBool("isCrouching", true);
            speed = c_speed;
            col_size.height = 0.25f;
            col_size.center = new Vector3(0, 0.08f, 0);
        }
        else if (isCrouching)
        {
            isCrouching = false;
            anim.SetBool("isCrouching", false);
            col_size.height = 0.5f;
            col_size.center = new Vector3(0, 0.28f, 0);
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;

        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);

        if(Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            anim.SetTrigger("isJumping");
            isCrouching = false;
            isGrounded = false;
            rb.AddForce(0, jumpHeight, 0);
        }

        if (isCrouching)
        {
            speed = w_speed;
            //Crouching controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = r_speed;
            //Running controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if(!isCrouching)
        {
            speed = w_speed;
            //Standing controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
	}

    void OnCollisionEnter()
    {
        isGrounded = true;
    }
}
