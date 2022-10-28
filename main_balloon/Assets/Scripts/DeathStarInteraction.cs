
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Audio;

public class DeathStarInteraction : MonoBehaviour
{
    //private Rigidbody rb; 
    public float force = 1;
    public float inflationRate = 1.1f;
    public float maxBalloonScale = 0.3f;
    public float height = 0.8f;
    public float DestroyTime = 1f;
    public GameObject explosionPrefab;
    public Renderer balloonRenderer;
    public GameObject deathstarPrefab;
    private AudioSource balloonPopAudio;
    public GameObject deathStarExplosion;

    Vector3 setHeight;
    Vector3 heightVector;
    Vector3 dirVec;
    Vector3 setPush;
    Vector3 pushVector;

    private float cubeSize = 0.1f;
    private int cubesInRow = 2;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    //controller velocity
    private ControllerVelocity controllerVelocity;
    public GameObject controller; 

    void Start()
    {   

        balloonRenderer = GetComponent<Renderer>();

        balloonRenderer.enabled = true;

       // rb = GetComponent<Rigidbody>(); //hämtar ballongens rigidbody

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
        //rb.AddForce(dirVec);
    }


    private void OnTriggerEnter(Collider other) {
        Vector3 bPos = transform.position; 
        Vector3 cPos = other.ClosestPoint(bPos); //closest point from the collider object
        this.GetComponent<CapsuleCollider>().enabled=false;
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position + new Vector3(0, -1, 0), explosionPrefab.transform.rotation);
        Destroy(gameObject);

    }

    private void MoveBalloon(Vector3 newDir, Vector3 bPos)
    {
        setPush = new Vector3(newDir.x, height-bPos.y, newDir.z); // sets a new direction after collision. height-bPos to never be above the ceiling
        heightVector = transform.position + setPush;
        //rb.AddForce(newDir*50f); //50 bör senare ändras till vilken kraft ballongen slås med 
        //rb.AddForce(newDir*(controllerVelocity.Velocity.x + controllerVelocity.Velocity.y + controllerVelocity.Velocity.z) *100);
    }
}
