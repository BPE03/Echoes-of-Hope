using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayerToCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam; // Drag your Cinemachine Camera in the inspector

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Find player
        virtualCam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
