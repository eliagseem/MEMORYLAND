using UnityEngine;
using System.Collections;

//Parts of script from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class npcScript : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float wanderRadius;
    public float wanderTimer;
    public bool isWandering = true;

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        anim = GetComponent<Animator>();
        //InvokeRepeating("RandomAnim", 2f, 2F);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWandering)
        timer += Time.deltaTime;

        if (timer >= wanderTimer && isWandering)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isMagic", false);
            timer = 0;
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
                    anim.SetBool("isMagic", false);
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
    }
}
