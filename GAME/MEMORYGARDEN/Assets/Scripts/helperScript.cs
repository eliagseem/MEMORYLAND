using UnityEngine;
using System.Collections;

//Parts of script from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class helperScript : MonoBehaviour
{
    private Animator anim;
    public GameObject dialogBox;
    public string[] randomThoughts = new string[3];
    public string[] desireThoughts = new string[3];
    public GameObject helperItem;

    private bool isTalking = false;
    private bool isThinking = false;
    private Transform target;
    private float timer;
    private float talkTimer;
    private float victoryTimer;
    private bool desiresMet = false;
    private AudioSource audioSource;


    // Use this for initialization
    void OnEnable()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTalking)
            timer += Time.deltaTime;

        if (isTalking)
            talkTimer += Time.deltaTime;

        if (talkTimer >= 5)
        {
            isTalking = !isTalking;
            talkTimer = 0;
            anim.SetBool("isIdle", true);
            anim.SetBool("isTalking", false);
        }

        if (timer >= 10)
        {
            var index = Random.Range(0, 3);
            dialogBox.GetComponent<TextMesh>().text = randomThoughts[index];
            timer = 0;
        }
    }

    public void revealItem()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        transform.LookAt(gos[0].transform);
        anim.SetBool("isIdle", false);
        anim.SetBool("isTalking", true);

        helperItem.SetActive(true);

        var index = Random.Range(0, 3);
        dialogBox.GetComponent<TextMesh>().text = desireThoughts[index];
        isTalking = true;
    }
}
