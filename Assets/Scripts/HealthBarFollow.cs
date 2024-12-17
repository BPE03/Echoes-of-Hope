using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, 0);

    void Update()
    {
        if (target != null)
        {
            // Update the position of the health bar
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }
}
