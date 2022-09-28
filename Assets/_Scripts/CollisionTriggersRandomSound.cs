using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggersRandomSound : MonoBehaviour
{
    //info
    [Header("This component requires a SoundManager.",order = 0)]

    private AudioSource _as;
    private SoundManager _soundManager;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        _soundManager.PlayCollisionSound(GetComponent<Collider>(), other.collider, _as);
            /*
            _as.clip = clipArray[Random.Range(0, clipArray.Length)];
            _as.Play(0);
            */
    }
}
