using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallUnlockScript : MonoBehaviour
{
    public GameObject Door2;
    public GameObject Door2Icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockLevel2()
    {
        Door2.SetActive(true);
        Door2Icon.SetActive(true);
    }
}
