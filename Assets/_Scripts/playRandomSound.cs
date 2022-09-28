using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRandomSound : MonoBehaviour
{
   private AudioSource _as;
    public AudioClip[] clipArray;
    public string objectTag;

    void Awake()
    {
        _as = GetComponent<AudioSource>();



    }

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == objectTag)
        {
            _as.clip = clipArray[Random.Range(0,clipArray.Length)];
            _as.Play(0);


        }
    }

}
