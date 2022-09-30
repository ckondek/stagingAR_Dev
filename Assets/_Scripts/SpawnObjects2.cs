using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnObjects2 : MonoBehaviour
{
    public float x1;
    public float x2;
    public float z1;
    public float z2;
    public float y;
    public float spawnTime;
    public float startAfter;
    public GameObject prefabToSpawn;
    ARSessionOrigin origin;
    
    void Start()
    {
        origin = GameObject.FindObjectOfType<ARSessionOrigin>();
        InvokeRepeating("SpawnThermos", startAfter, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
      

    }

    private void SpawnThermos()
    {
        var newObj = Instantiate(prefabToSpawn, new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2)), Quaternion.identity);
        newObj.transform.parent = origin.transform;
    }


}
