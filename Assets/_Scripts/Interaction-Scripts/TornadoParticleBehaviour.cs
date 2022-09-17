using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoParticleBehaviour : MonoBehaviour
{
    GameObject objectToCircle;
    float speed;
    float radius;
    float maxHeight;
    float riseDivisor;

    TornadoBehaviour tornadoBehaviour;
    private float counter;


    void Start()
    {
        GameObject tornadoManager = GameObject.Find("TornadoManager");
        tornadoBehaviour = tornadoManager.GetComponent<TornadoBehaviour>();
        objectToCircle = tornadoBehaviour.objectToCircle;
        speed = tornadoBehaviour.speed;
        radius = tornadoBehaviour.radius;
        maxHeight = tornadoBehaviour.maxHeight;
        riseDivisor = tornadoBehaviour.riseDivisor;
        counter = 0.0f;
    }
  
    void FixedUpdate()
    {
        counter += speed/100.0f;
        transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius),
                                             objectToCircle.transform.position.y + counter/riseDivisor,
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
        radius+= 0.001f;

        if (transform.position.y > maxHeight) Destroy(gameObject);
     
    }

}
