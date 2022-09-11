using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BalloonFloatingBehavior : MonoBehaviour
{

    // public Transform target;
    public GameObject targetGO;
    public Rigidbody rb;
    //public float amplitude = 0.1f;
    // public float frequency = 1f;
    public float force;

    public float inflationRate = 1.1f;
    public float maxBalloonScale = 1.0f;


    //Vector3 posOffset = new Vector3 ();
    //Vector3 tempPos = new Vector3 ();

    void Start()
    {
        // Store the starting position of the object
        //posOffset = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        //rb.AddForce(transform.up * 1, ForceMode.Acceleration);

        // Float up/down with a Sin()
        //tempPos = posOffset;
        //tempPos.y += 10;
        // tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        //transform.position = tempPos;

        if (transform.localScale.x < maxBalloonScale)
        {
            transform.localScale += transform.localScale * inflationRate * Time.deltaTime;
        }

        targetGO = GameObject.Find("target");

        Vector3 f = targetGO.transform.position - transform.position;
        f = f.normalized;
        f = f * force;
        rb.AddForce(f);
    }
}
