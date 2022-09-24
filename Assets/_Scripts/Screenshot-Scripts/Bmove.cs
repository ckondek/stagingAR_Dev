using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bmove : MonoBehaviour
{
    public float forceMult = 5;
    public float lift = 5;
    public float spin = 4;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * forceMult * Time.deltaTime);
        rb.AddForce(transform.up * lift * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * spin);
    }
}
