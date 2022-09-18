using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAround : MonoBehaviour
{
    public GameObject objectToCircle;
    public float speed;
    public float radius;

    private float counter;


    void Start()
    {
        counter = 0.0f;
    }
  
    void Update()
    {
        counter += speed * Time.deltaTime;
        transform.position = new Vector3(
                                             objectToCircle.transform.position.x + ((float)Mathf.Sin(counter) * radius),
                                             objectToCircle.transform.position.y,
                                             objectToCircle.transform.position.z + ((float)Mathf.Cos(counter) * radius)
                                        );
     
    }
}
