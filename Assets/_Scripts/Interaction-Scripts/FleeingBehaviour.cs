using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingBehaviour : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 0.1f;
    public float minDistance = 2.0f;

    [Tooltip("how long should the object flee after player comes close?")]
    public int fleeIterations = 100;

    GameObject player;
    bool fleeing = false;
    float distance;
    Vector3 direction;
    int fleetime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CameraBody");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed Update runs once per physics update
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < minDistance || fleetime > 0) 
        {
            if (fleetime == 0) fleetime = fleeIterations;
            direction = transform.position - player.transform.position;
            transform.Translate(direction * speed * Time.deltaTime);
            fleetime --;
        }
    }

}
