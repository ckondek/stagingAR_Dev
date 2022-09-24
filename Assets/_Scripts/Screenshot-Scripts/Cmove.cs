using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmove : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 _velocity;
    public Vector3 _spin;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _velocity = new Vector3(0.2f, 0.2f, 0.2f);
        _spin = new Vector3(5, 5, 5);
       
        rb.AddForce(_velocity, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion _rotation = Quaternion.Euler(_spin * Time.deltaTime);
        rb.MoveRotation(rb.rotation * _rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 reflectVector = collision.contacts[0].normal;
        _velocity = Vector3.Reflect(_velocity, reflectVector);
        rb.velocity = _velocity;
       
    }
}
