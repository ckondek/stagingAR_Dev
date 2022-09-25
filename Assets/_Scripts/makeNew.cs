using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class makeNew : MonoBehaviour
{
    public GameObject newObject;
    public float spawnTime;
    public float start;
    private float counter;
    private float timer;
   // private ARSessionOrigin origin;
   
    void Start()
    {
        counter = 0f;
        timer = 0f;
       // origin = GameObject.FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        counter += Time.deltaTime;

        if (timer > start)
        {
            if (counter > spawnTime)
            {

              // this.transform.position = origin.transform.position;
               Instantiate(newObject,
                              new Vector3(0.0f, 4.0f, 0.0f),
                              Quaternion.identity);

               // spawned.transform.parent = origin.transform;

                counter = 0f;
            }



        }

    }
}
