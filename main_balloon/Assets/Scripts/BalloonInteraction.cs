using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonInteraction : MonoBehaviour
{
    private Rigidbody rb; 
    public float force = 1;
    public float inflationRate = 1.1f;
    public float maxBalloonScale = 0.3f;
    public float height = 0.8f;
    public GameObject confettiExplosionPrefab;
    public Renderer balloonRenderer;

    Vector3 setHeight;
    Vector3 heightVector;
    Vector3 dirVec;

    void Start()
    {
        balloonRenderer = GetComponent<Renderer>();
        balloonRenderer.enabled = true;

        rb = GetComponent<Rigidbody>(); //hämtar ballongens rigidbody
        setHeight = new Vector3(0, height, 0); 
        heightVector = transform.position + setHeight; //punkt som ballongerna dras till rakt ovanför sig
    }

    void FixedUpdate()
    {
        if (transform.localScale.x < maxBalloonScale) //blåser upp ballongerna
        {
            transform.localScale += transform.localScale * inflationRate * Time.deltaTime;
        }

        dirVec = heightVector - transform.position; //transfrom.position är bara ballongens pos
        dirVec = dirVec.normalized;
        dirVec = dirVec * force; 
        rb.AddForce(dirVec);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Sword") 
        {
            PopBalloon();
        }
        if (other.gameObject.name == "Glove") 
        {
            Vector3 bPos = transform.position; 
            Vector3 gwPos = other.ClosestPoint(bPos); //closest point from the glove
            Vector3 GloveDir = (bPos - gwPos).normalized; //normalized vector between balloon and closest sword point
            MoveBalloon(GloveDir);
        }
    }

    private void PopBalloon()
    {
        AudioSource balloonPopAudio = GetComponent<AudioSource>();
        balloonPopAudio.Play();
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        Destroy(confettiExplosion, 1f);
        balloonRenderer.enabled = false;
        Destroy(gameObject, balloonPopAudio.clip.length);
    }

     private void MoveBalloon(Vector3 newDir)
    {
        setHeight = new Vector3(newDir.x, 0, 0);
        heightVector = transform.position + setHeight;
        rb.AddForce(newDir*50f);
    }
}
