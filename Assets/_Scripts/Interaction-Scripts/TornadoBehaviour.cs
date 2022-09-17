using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBehaviour : MonoBehaviour
{
    // the object at the center of the tornado
    //public GameObject tornadoCenterObject;
    // object which is spawned many times and forms a tornado
    public GameObject tornadoParticle;
    public GameObject objectToCircle;
    public float speed;
    public float radius;
    public float maxHeight;
    public float riseDivisor;

    // how many fixed updates need to happen before the next object is spawned
    public int spawnCooldown;

    private int buffer, maxr; 

    void Start()
    {
        buffer = spawnCooldown;
        maxr = (int) spawnCooldown/2;
    }
  
    void FixedUpdate()
    {
        if (spawnCooldown == 0)
        {
            Instantiate(tornadoParticle, new Vector3(0, 0, 0), Random.rotation);
            spawnCooldown = buffer + Random.Range(-maxr, maxr);
        }
        else 
        {
            spawnCooldown --;
        }
    }
}
