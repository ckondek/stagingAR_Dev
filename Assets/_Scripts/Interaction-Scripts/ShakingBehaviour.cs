//based on source: https://gist.github.com/st4rdog/82a4d99c4f6eb59efa162a05ec62163b

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingBehaviour : MonoBehaviour
{
    [Header("Info")]
   private Vector3 _startPos;
   private Vector3 _randomPos;
   private bool shaking = false;
   Transform playerTransform;
 
   [Header("Settings")]
   [Range(0f, 2f)]
   public float _distance = 0.1f;

   //Fixed Update runs once per physics update
    void FixedUpdate()
    {
        if (shaking)
        {
            _randomPos = _startPos + (Random.insideUnitSphere * _distance);
 
           transform.position = _randomPos;
 
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTransform = other.transform;
            _startPos = transform.position;
            shaking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shaking = false;
        }
    }
 
}
