using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public AudioClip chipHit;
    public AudioClip[] clipArray;
    private HashSet<Collider> _collisions = new HashSet<Collider>();
 
     public void Update()
     {
         _collisions.Clear();
     }
 
     public void PlayCollisionSound(Collider colla, Collider collb, AudioSource audioSource)
     {
         if (_collisions.Contains(colla) && _collisions.Contains(collb))
         {
             return;
         }    
 
         _collisions.Add(colla);
         _collisions.Add(collb);
 
         audioSource.clip = clipArray[Random.Range(0, clipArray.Length)];;
         audioSource.Play();
     }
}
