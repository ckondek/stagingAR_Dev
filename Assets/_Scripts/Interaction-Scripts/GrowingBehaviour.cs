using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehaviour : MonoBehaviour
{
    bool growing = false;
    public Vector3 growth;
    public float maxSize;
    public float minSize;
    

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
        if (growing)
        {
            if (transform.localScale.y < maxSize)
            {
                transform.localScale += growth;
            }
        }
        else
        {
            if (transform.localScale.y > growth.y+minSize)
            {
                transform.localScale -= growth;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            growing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            growing = false;
        }
    }
}
