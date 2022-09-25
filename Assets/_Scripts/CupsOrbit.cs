using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsOrbit : MonoBehaviour
{
    public GameObject objectToCircle, objectToSpawn;
    public float speed;
    public float radius;

    private float counter;
    private GameObject cup1, cup2, cup3, cup4, cup5;


    void Start()
    {
        counter = 0.0f;
        cup1 = GameObject.Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        cup2 = GameObject.Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        cup3 = GameObject.Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        cup4 = GameObject.Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        cup5 = GameObject.Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);

    }

    void Update()
    {
        counter += speed * Time.deltaTime;
        cup1.transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius),
                                             objectToCircle.transform.position.y,
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
        cup2.transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius / 2.0f),
                                             objectToCircle.transform.position.y + ((float)Mathf.Sin(counter) * radius / 2.0f),
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
        cup3.transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius / -2.0f),
                                             objectToCircle.transform.position.y + ((float)Mathf.Sin(counter) * radius / -2.0f),
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
        cup4.transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius / -2.0f),
                                             objectToCircle.transform.position.y + ((float)Mathf.Sin(counter) * radius / 2.0f),
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
        cup5.transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius / 2.0f),
                                             objectToCircle.transform.position.y + ((float)Mathf.Sin(counter) * radius / -2.0f),
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );

    }
}
