using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 0.1f;
    [Tooltip("0 is following on player-touch, 1 is following without trigger")]
    public int mode = 0; 

    GameObject player;
    bool following = false;

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
        if (mode == 1 || following)
        {
            // Move sphere position a step closer to the player.
            var step =  speed * Time.deltaTime; // calculate distance to move
            transform.position =  Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mode == 0 && other.tag == "Player")
        {
            following = true;
        }
    }

}
