using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingBehaviour : MonoBehaviour
{
    [Header("Info")]
   private Vector3 _startPos;
   private Vector3 _randomPos;
    private Vector3 _randNoY;
    private float _maxdist, _currentdist, _distance;
   private bool shaking = false;
   Transform playerTransform;
 
   [Header("Settings")]
   [Range(0f, 0.1f)]
   public float _maxShakeDistance = 0.1f;

    void Start()
    {
        _startPos = transform.position;
        _distance = 0.001f;
    }
   //Fixed Update runs once per physics update
    void FixedUpdate()
    {
        if (shaking)
        {
            _currentdist = Vector3.Distance(playerTransform.position, transform.position);
            _currentdist = Mathf.Abs(_currentdist);
            if (_currentdist!=_maxdist)
            {
                _distance = _maxShakeDistance  * ((_maxdist-_currentdist)/_maxdist);
            }
            
            _randNoY = new Vector3(Random.Range(0.0f, 1.0f) * _distance, 0.0f, Random.Range(0.0f, 1.0f) * _distance);
            _randomPos = _startPos + _randNoY;
            transform.position = _randomPos;
        }
        
        
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShake")
        {
            playerTransform = other.transform;
            _startPos = transform.position;
            _maxdist = Vector3.Distance(playerTransform.position, _startPos);
            _maxdist = Mathf.Abs(_maxdist);
            shaking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShake")
        {
            shaking = false;
        }
    }
 
}
