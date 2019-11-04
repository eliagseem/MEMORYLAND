﻿using UnityEngine;
using System.Collections;

//Parts of script from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class npcScript : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float wanderRadius;
    public float wanderTimer;
    public bool isWandering = true;
    public GameObject dialogBox;
    public string[] randomThoughts = new string[5];
    public string[] desireThoughts = new string[5];
    public GameObject desiredObject;

    private bool isTalking = false;
    private bool isThinking = false;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;
    private float talkTimer;
    private AudioSource audioSource;

    // Use this for initialization
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWandering)
            timer += Time.deltaTime;

        if (isTalking)
            talkTimer += Time.deltaTime;

        if(talkTimer >= 5)
            startWandering();

        if (timer >= wanderTimer && isWandering)
        {
            isThinking = false;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            timer = 0;
            dialogBox.GetComponent<TextMesh>().text = string.Empty;
        }

        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalking", false);

                    if (!isTalking && !isThinking)
                    {
                        var index = Random.Range(0, 5);
                        dialogBox.GetComponent<TextMesh>().text = randomThoughts[index];
                        isThinking = true;
                    }
                }
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void stopWandering()
    {
        agent.isStopped = true;
        agent.ResetPath();
        isWandering = false;
        timer = 0;
    }

    public void startWandering()
    {
        agent.isStopped = false;
        isWandering = true;
        anim.SetBool("isTalking", false);
    }

    public void talkToPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        transform.LookAt(gos[0].transform);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isTalking", true);

        var index = Random.Range(0, 5);
        dialogBox.GetComponent<TextMesh>().text = desireThoughts[index];
        isTalking = true;
        audioSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DesiredObject" && collision.gameObject == desiredObject)
        {
            Destroy(collision.gameObject);
            //play a sound now that the character got the object
            audioSource.Play();
        }
    }
}
