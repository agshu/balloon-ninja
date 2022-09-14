using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BalloonFloatingBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 1;
    public float inflationRate = 1.1f;
    public float maxBalloonScale = 0.3f;
    public float height = 0.8f;

    Vector3 setHeight;
    Vector3 heightVector;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        setHeight = new Vector3(0, height, 0);
        heightVector = transform.position + setHeight;
       
    }

    void FixedUpdate()
    {
        if (transform.localScale.x < maxBalloonScale)
        {
            transform.localScale += transform.localScale * inflationRate * Time.deltaTime;
        }


        Vector3 f = heightVector - transform.position;
        f = f.normalized;
        f = f * force;
        rb.AddForce(f);
    }
}
