using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonPopping : MonoBehaviour
{

    public GameObject confettiExplosionPrefab;

    public Renderer balloonRenderer;

    void Start()
    {
        balloonRenderer = GetComponent<Renderer>();
        balloonRenderer.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Sword" || collision.collider.name == "target")
        {
            PopBalloon();
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
}
