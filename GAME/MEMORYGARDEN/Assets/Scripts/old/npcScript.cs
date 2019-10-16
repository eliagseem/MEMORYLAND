using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcScript : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private GameObject player;
    private GameObject npcChar;
    private bool canSeePlayer = false;
    private float transitionSpeed = 2f;
    private bool isMoving = false;
    private float nextActionTime = 0.0f;
    public Transform _destination;
    public float period = 15.00f;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    private NavMeshAgent _navMeshagent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        StartCoroutine(moveToPosition());

    }

    // Update is called once per frame
    void Update()
    {
        //if (!isMoving)
        //{
        //    StartCoroutine(moveToPosition());
        //    isMoving = false;
        //}
        //else
        //    StartCoroutine(rest());

        //timer += Time.deltaTime;

        //if (timer >= wanderTimer)
        //{
        //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //    agent.SetDestination(newPos);
        //    timer = 0;
        //}
    }

    //private void SetDestination()
    //{
    //    if (_destination != null)
    //    {
    //        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
    //        agent.SetDestination(newPos);
    //        timer = 0;
    //    }
    //}

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    IEnumerator moveToPosition()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        isMoving = true;
        Debug.Log("moving");

        while (true)
        {
            Vector3 newPos = new Vector3(transform.position.x + 0.5f, 0, 0);
            transform.position = Vector3.Slerp(transform.position, newPos, 0.1f);
            yield return new WaitForSeconds(5.00f);
        }
    }

    IEnumerator rest()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        Debug.Log("not moving");

        while (true)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(5.00f);
            isMoving = true;
        }
    }
}