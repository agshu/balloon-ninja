using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class float2 : MonoBehaviour {

    public Transform target;
    public Rigidbody rb;
    //public float amplitude = 0.1f;
   // public float frequency = 1f;
    public float force;
    //Vector3 posOffset = new Vector3 ();
    //Vector3 tempPos = new Vector3 ();

    void Start () {
        // Store the starting position of the object
        //posOffset = transform.position;
        rb = GetComponent<Rigidbody>();
      
    }
     
    void FixedUpdate () {
        //rb.AddForce(transform.up * 1, ForceMode.Acceleration);
        
        // Float up/down with a Sin()
        //tempPos = posOffset;
        //tempPos.y += 10;
        // tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        //transform.position = tempPos;

        Vector3 f = target.position - transform.position;
        f = f.normalized;
        f = f * force;
        rb.AddForce(f);
        }
}
 