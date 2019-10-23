using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public float distance;

    public void Update()
    {
        var cam = GetComponent<Transform>();
        cam.position = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z - distance);
    }
}
