using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private float camYoffset, camZoffset;
    public GameObject player;
    void Start()
    {
        camYoffset = 2.82f;
        camZoffset = -3.73f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCamPos = new Vector3(player.transform.position.x, 17085.76f, player.transform.position.z + camZoffset);
        //Quaternion camRotation = player.transform.rotation;
        transform.position = newCamPos;
        transform.LookAt(player.transform.position);
        //transform.rotation = camRotation;
    }
}
