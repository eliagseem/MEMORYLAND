using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charDeManager : MonoBehaviour
{
    private inputManager _inputMan;


    // Start is called before the first frame update
    void Start()
    {
        _inputMan = GetComponent<inputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        //_inputMan.m_soundPitch
    }
}
