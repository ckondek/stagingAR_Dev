using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingWithSoundBehaviour : MonoBehaviour
{
    bool growing = false;
    public float growthCoefficient = 0.1f, shrinkCoefficient = 0.1f;
    private Vector3 growth, shrink;
    public float maxSize;
    public float minSize;
    AudioSource _as;
    public AudioClip growSound;
    public AudioClip shrinkSound;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        growth = new Vector3(growthCoefficient, growthCoefficient, growthCoefficient);
        shrink = new Vector3(shrinkCoefficient, shrinkCoefficient, shrinkCoefficient);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed Update runs once per physics update
    void FixedUpdate()
    {
        if (growing)
        {
            if (transform.localScale.y < maxSize)
            {
                transform.localScale += growth;
            }
        }
        else
        {
            if (transform.localScale.y > shrink.y+minSize)
            {
                transform.localScale -= shrink;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            growing = true;
            _as.clip = growSound;
            _as.Play(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            growing = false;
            _as.clip = shrinkSound;
            _as.Play(0);
        }
    }
}

