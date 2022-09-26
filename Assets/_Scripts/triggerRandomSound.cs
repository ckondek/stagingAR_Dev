using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerRandomSound : MonoBehaviour
{
    private AudioSource _as;
    public AudioClip[] clipArray;
   

    void Awake()
    {
        _as = GetComponent<AudioSource>();



    }

    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

       
            _as.clip = clipArray[Random.Range(0, clipArray.Length)];
            _as.Play(0);


        
    }

}
