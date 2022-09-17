using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private float time;
    public float x1;
    public float x2;
    public float z1;
    public float z2;
    public float y;
    public float spawnTime;
    public GameObject prefabToSpawn;
    void Start()
    {
        time = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnTime)
        {
            Instantiate(prefabToSpawn, new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2)), Quaternion.identity);
            time = 0f;
        }

    }


}