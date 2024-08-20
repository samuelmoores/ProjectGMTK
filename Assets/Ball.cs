using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] bouncingSounds;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 10.0f)
        {
            source.clip = bouncingSounds[0];
            source.Play();

        }
        else if(rb.velocity.magnitude < 10.0f && rb.velocity.magnitude > 5.0f)
        {
            source.clip = bouncingSounds[1];
            source.Play();
        }
        else if (rb.velocity.magnitude < 5.0f && rb.velocity.magnitude > 1.0f)
        {
            source.clip = bouncingSounds[2];
            source.Play();
        }
    }
}
