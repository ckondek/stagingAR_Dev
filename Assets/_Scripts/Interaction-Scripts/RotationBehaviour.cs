using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    Transform playerTransform;
    float distance;
    bool rotating = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed Update runs once per physics update
    void FixedUpdate()
    {
        if (rotating)
        {
            distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance > 0)
            {
                transform.Rotate(0.5f*(1/distance), 0.9f*(1/distance), 0.0f, Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTransform = other.transform;
            rotating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rotating = false;
        }
    }
}
