using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonPopping : MonoBehaviour
{

    public GameObject confettiExplosionPrefab;
    public AudioSource PopAudioSource;

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
            PopAudioSource.Play();
            PopBalloon();
        }

    }

    private void PopBalloon()
    {
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        Destroy(confettiExplosion, 1f);
        balloonRenderer.enabled = false;

        Destroy(gameObject, PopAudioSource.clip.length);
    }
}
