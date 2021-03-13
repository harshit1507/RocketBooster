using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip thrusterSound;

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainBoosterParticle.isPlaying)
            {
                mainBoosterParticle.Play();
            }
            
        }
        else
        {
            audioSource.Stop();
            mainBoosterParticle.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            audioSource.PlayOneShot(thrusterSound);
            if(!rightThrusterParticle.isPlaying)
            {
                rightThrusterParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            audioSource.PlayOneShot(thrusterSound);
            if (!leftThrusterParticle.isPlaying)
            {
                leftThrusterParticle.Play();
            }
        }
        else
        {
            rightThrusterParticle.Stop();
            leftThrusterParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        
        rb.freezeRotation = true;   // freezing rotation so we can manuallly rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;   // unfreezing rotation so the physics system take over
    }
}
