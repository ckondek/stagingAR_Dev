using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeNew : MonoBehaviour
{
    public GameObject newObject;
    public float spawnTime;
    public float start;
    private float counter;
    private float timer;
    void Start()
    {
        counter = 0f;
        timer = 0f;
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


                Instantiate(newObject,
                              new Vector3(0.0f, 4.0f, 0.0f),
                              Quaternion.identity);

                counter = 0f;
            }



        }

    }
}
