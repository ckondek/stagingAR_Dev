using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolve : MonoBehaviour
{
    public float speed1;
    void Start()
    {
        speed1 = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed1, 0);
    }
}
