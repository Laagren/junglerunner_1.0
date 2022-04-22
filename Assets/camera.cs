using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineTransposer offset;
    private float offsetTimer;
    public float desiredOffset = -4f;
    public float startOffset = -18.0f;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        offset = cam.GetCinemachineComponent<CinemachineTransposer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (offsetTimer < 2 || offset.m_FollowOffset.z <= desiredOffset)
        {
            offsetTimer += Time.deltaTime;
            offset.m_FollowOffset.z += 0.005f;
        }
        //if (offset.m_FollowOffset.z  <= desiredOffset)
        //{
        //    
        //}
    }
}
