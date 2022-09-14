using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopping : MonoBehaviour
{

    public GameObject confettiExplosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        Console.log(collision.collider.name);
        if (collision.collider.name == "Sword" || collision.collider.name == "target")
        {
            PopBalloon();
        }

    }

    private void PopBalloon()
    {
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        Destroy(confettiExplosion, 1f);
            
        Destroy(gameObject);
    }
}
