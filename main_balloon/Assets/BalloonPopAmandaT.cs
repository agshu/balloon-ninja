using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "target")
        {
            PopBalloon();
        }

    }

    private void PopBalloon()
    {
        Destroy(gameObject);
    }
}
