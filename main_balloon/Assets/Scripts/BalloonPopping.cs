using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopping : MonoBehaviour
{

    public GameObject confettiExplosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Sword" || collision.collider.name == "target")
        {
            PopBalloon();
        }

    }

    private void PopBalloon()
    {
        Debug.Log("ballong position" + gameObject.transform.position);
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        Debug.Log("confetti position" + confettiExplosion.transform.position);

        Destroy(confettiExplosion, 1f);
            
        Destroy(gameObject);
    }
}
