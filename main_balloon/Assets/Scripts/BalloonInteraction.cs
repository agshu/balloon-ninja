
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
    public GameObject explosionPrefab;
    public GameObject waterSplashPrefab;
    public Renderer balloonRenderer;
    private AudioSource balloonPopAudio;
    public GameObject balls;
    public GameObject discoBallPrefab;
    public GameObject deathstarPrefab;
    public GameObject magnet;

    Vector3 setHeight;
    Vector3 heightVector;
    Vector3 dirVec;
    Vector3 newDirPoint;
    Vector3 magnetPos;

    private float cubeSize = 0.1f;
    private int cubesInRow = 2;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    //controller velocity
    private ControllerVelocity controllerVelocity;
    public GameObject controller; 

    void Start()
    {   
        controller = GameObject.FindWithTag("Controller");
        controllerVelocity = controller.GetComponent<ControllerVelocity>();

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
        Vector3 bPos = transform.position; 
        Vector3 cPos = other.ClosestPoint(bPos); //closest point from the collider object
        Vector3 forceDir = (bPos - cPos).normalized; //normalized vector between balloon and closest hit point

        if (other.gameObject.name == "Sharp" || other.gameObject.name == "spikes" ) 
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
            if (gameObject.name == "BalloonPrefabDisco(Clone)")
            {
                DiscoBalloon();
            }
            if (gameObject.name == "BalloonPrefabDeathStar(Clone)")
            {
                DeathStarSpawn();
            }

        }
        if ( other.gameObject.name == "Wall" || other.gameObject.name == "Body") 
        {
            MoveBalloon(forceDir, bPos);
        }

        if (other.gameObject.name == "Glove" || other.gameObject.name == "Handle" || other.gameObject.name == "Blade" || other.gameObject.name == "pot" || other.gameObject.name == "cactusbody" ) 
        {
            HitBalloon(bPos);
        }

        if (other.gameObject.name == "MagnetHitBox")
        {
            DrawBalloonBack(bPos);
        }
    }

    private void PopBalloon()
    {
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, explosionPrefab.transform.rotation);
        Destroy(explosion, DestroyTime);
        balloonRenderer.enabled = false; //detta är för poolen
        Destroy(gameObject, balloonPopAudio.clip.length);
    }

    private void DiscoBalloon()
    {
        GameObject discoBall = Instantiate(discoBallPrefab, gameObject.transform.position, discoBallPrefab.transform.rotation);
        Destroy(gameObject);
        Destroy(discoBall, 5.5f);
    }

    private void DeathStarSpawn()
    {
        GameObject deathStar = Instantiate(deathstarPrefab, new Vector3(-4f, 1.5f, 0), deathstarPrefab.transform.rotation);
        Destroy(gameObject);
    }

    private void PopBalloonWater(Vector3 bPos)
    {
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, explosionPrefab.transform.rotation);
        GameObject splashExplosion = Instantiate(waterSplashPrefab, new Vector3(bPos.x, 1, bPos.z), explosionPrefab.transform.rotation);

        Destroy(explosion, DestroyTime);
        Destroy(splashExplosion, DestroyTime);
        balloonRenderer.enabled = false;
        Destroy(gameObject);
    }

    private void MoveBalloon(Vector3 newDir, Vector3 bPos) 
    {
        newDirPoint = new Vector3(newDir.x, height-bPos.y, newDir.z); // sets a new direction after collision. height-bPos to never be above the ceiling
        heightVector = transform.position + newDirPoint;
        rb.AddForce(newDirPoint*50f);
    }

    private void HitBalloon(Vector3 bPos)
    {
        newDirPoint = new Vector3(controllerVelocity.Velocity.x, height-bPos.y, controllerVelocity.Velocity.z); // sets a new direction after collision. height-bPos to never be above the ceiling
        heightVector = transform.position + newDirPoint;
        rb.AddForce(controllerVelocity.Velocity*20); //AddForce is a Vec3
    }

    private void DrawBalloonBack(Vector3 bPos)  // with magnet
    {
        magnet = GameObject.FindWithTag("Magnet");
        magnetPos = magnet.transform.position;
        Vector3 dir = (magnetPos - bPos).normalized;
        newDirPoint = new Vector3(dir.x, height-bPos.y, dir.z);
        heightVector = transform.position/2 + newDirPoint;
        rb.AddForce(newDirPoint*70f);
    }

    public void explode(Vector3 SwordDir) 
    {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

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
