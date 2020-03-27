using UnityEngine;
using System.Collections;

//Parts of script from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class npcScriptCredits : MonoBehaviour
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
    public Camera cutsceneCamera;
    public Camera mainCamera;
    public GameObject successItem;
    public AudioClip[] voiceClips = new AudioClip[5];
    public GameObject particleSys;
    public AudioClip deathSound;
    public AudioSource musicSource;
    public AudioClip victorySound;

    private bool isTalking = false;
    private bool isThinking = false;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;
    private float talkTimer;
    private float victoryTimer;
    private bool desiresMet = false;
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

        if (desiresMet)
            victoryTimer += Time.deltaTime;

        if (talkTimer >= 5)
            startWandering();

        if(victoryTimer >= 10 && desiresMet)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
            cutsceneCamera.enabled = false;
            mainCamera.enabled = true;
            particleSys.SetActive(false);
            anim.SetBool("isDancing", false);
            Instantiate(successItem, transform.position, transform.rotation);
            desiresMet = false;
            Destroy(this.gameObject);
        }

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

                    if (!isTalking && !isThinking && !desiresMet)
                    {
                        var index = Random.Range(0, 5);
                        dialogBox.GetComponent<TextMesh>().text = randomThoughts[index];
                        audioSource.clip = voiceClips[index];
                        audioSource.Play();
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
        talkTimer = 0;
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
        audioSource.clip = voiceClips[index];
        audioSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DesiredObject" && collision.gameObject == desiredObject)
        {
            Destroy(collision.gameObject);
            audioSource.clip = voiceClips[4];
            audioSource.Play();
            musicSource.clip = victorySound;
            musicSource.Play();
            stopWandering();
            anim.SetBool("isDancing", true);
            particleSys.SetActive(true);
            cutsceneCamera.enabled = true;
            mainCamera.enabled = false;
            desiresMet = true;
            dialogBox.GetComponent<TextMesh>().text = "";
        }
    }
}
