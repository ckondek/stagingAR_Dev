using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnObjects2 : MonoBehaviour
{
    private float time;
    public float x1;
    public float x2;
    public float z1;
    public float z2;
    public float y;
    public float spawnTime;
    public float startAfter;
    private bool spawning;
    public GameObject prefabToSpawn;
    ARSessionOrigin origin;
    
    void Start()
    {
        time = 0f;
        origin = GameObject.FindObjectOfType<ARSessionOrigin>();
        spawning = false;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > startAfter) { spawning = true; }
        if (spawning) {
            if (time >= spawnTime)
            {
                var newObj = Instantiate(prefabToSpawn, new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2)), Quaternion.identity);
                newObj.transform.parent = origin.transform;

                time = 0f;
            }





        }

      

    }


}
