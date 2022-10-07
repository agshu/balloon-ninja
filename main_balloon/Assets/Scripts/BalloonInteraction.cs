
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonInteraction : MonoBehaviour
{
    private Rigidbody rb; 
    public float force = 1;
    public float inflationRate = 1.1f;
    public float maxBalloonScale = 0.3f;
    public float height = 0.8f;
    public float DestroyTime = 1f;
    public GameObject confettiExplosionPrefab;
    public GameObject waterSplashPrefab;
    public Renderer balloonRenderer;
    private AudioSource balloonPopAudio;
    public GameObject balls;

    Vector3 setHeight;
    Vector3 heightVector;
    Vector3 dirVec;
    Vector3 setPush;
    Vector3 pushVector;

    private float cubeSize = 0.1f;
    private int cubesInRow = 2;

    float cubesPivotDistance;
    Vector3 cubesPivot;

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
        //Debug.Log(other.gameObject.name);
        Vector3 bPos = transform.position; 
        Vector3 cPos = other.ClosestPoint(bPos); //closest point from the collider object
        Vector3 forceDir = (bPos - cPos).normalized; //normalized vector between balloon and closest sword point

        if (other.gameObject.name == "Sharp") 
        {
            balloonPopAudio = GetComponent<AudioSource>();
            balloonPopAudio.Play();
            this.GetComponent<MeshCollider>().enabled=false; //disables mesh collider so that you can't hit the object multiple times
            if (gameObject.name == "BalloonPrefab(Clone)")
            {
                PopBalloon();
            }
            if (gameObject.name == "BalloonPrefabPaint(Clone)")
            {
                PopBalloon();
            }
            if (gameObject.name == "ballExplosion(Clone)")
            {
                explode(forceDir);
            }
            if (gameObject.name == "BalloonPrefabWater(Clone)")
            {
                PopBalloonWater(bPos);
            }
            if (gameObject.name == "BalloonPrefabBluePaint(Clone)")
            {
                PopBalloon();
            }

        }
        if (other.gameObject.name == "Glove" || other.gameObject.name == "Wall" || other.gameObject.name == "Body") 
        {
            MoveBalloon(forceDir, bPos);
        }
    }

    private void PopBalloon()
    {
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        Destroy(confettiExplosion, DestroyTime);
        balloonRenderer.enabled = false;
        Destroy(gameObject, balloonPopAudio.clip.length);
    }

    private void PopBalloonWater(Vector3 bPos)
    {
        GameObject confettiExplosion = Instantiate(confettiExplosionPrefab, gameObject.transform.position, confettiExplosionPrefab.transform.rotation);
        GameObject splashExplosion = Instantiate(waterSplashPrefab, new Vector3(bPos.x, 1, bPos.z), confettiExplosionPrefab.transform.rotation);

        Destroy(confettiExplosion, DestroyTime);
        Destroy(splashExplosion, DestroyTime);
        balloonRenderer.enabled = false;
        Destroy(gameObject, balloonPopAudio.clip.length);
    }

    private void MoveBalloon(Vector3 newDir, Vector3 bPos)
    {
        setPush = new Vector3(newDir.x, height-bPos.y, 0); // sets a new direction after collision. height-bPos to never be above the ceiling
        heightVector = transform.position + setPush;
        rb.AddForce(newDir*50f); //50 bör senare ändras till vilken kraft ballongen slås med 
        //Debug.Log(heightVector);
    }

    public void explode(Vector3 SwordDir) 
    {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        //make object disappear
        //gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z, SwordDir);
                }
            }
        }
        balloonRenderer.enabled = false;
        Debug.Log("HEST");
        Destroy(gameObject, balloonPopAudio.clip.length);
    }

    void createPiece(int x, int y, int z, Vector3 SwordDir) {
        //create piece
        GameObject piece;
        
        piece = Instantiate(balls, new Vector3(0, 0, 0), Quaternion.identity);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody, set mass and add force in direction of sword hit
        Rigidbody body = piece.AddComponent<Rigidbody>();
        body.mass = cubeSize;
        body.AddForce(SwordDir*50f+ Random.onUnitSphere * 25.0f);

    }



}
