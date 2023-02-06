using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public class BalloonGenerator : MonoBehaviour
{
    public float spawnTime = 2f;
    public int maxNumBalloons = 30;
    private GameObject[] spawnedBalloons;
    private string[] messageArray;
    private static List<string> colors = new List<string>() { "red", "blue", "green", "pink" };
    private static List<string> explosions = new List<string>() { "confetti", "water", "paint", "marbles", "disco", "deathstar"};

    // Balloon options
    public GameObject confettiBalloonPrefab;
    public GameObject waterBalloonPrefab;
    public GameObject paintBalloonPrefab;
    public GameObject marblesBalloonPrefab;
    public GameObject discoBalloonPrefab;
    public GameObject deathstarBalloonPrefab;

    // Material options
    public Material redBalloonMat;
    public Material blueBalloonMat;
    public Material greenBalloonMat;
    public Material pinkBalloonMat;

    WebSocket ws;

    void Start()
    {
        InvokeRepeating("BalloonSpawn", spawnTime, spawnTime);
        ws = new WebSocket("ws://partypoppervr.herokuapp.com/");

        ws.Connect();

        if (ws == null)
        {
            UnityEngine.Debug.Log("Not connected");

            return;
        }
        else
        {
            UnityEngine.Debug.Log(ws.Url);
        }

        ws.OnMessage += (sender, e) =>
        {
            incoming_messages.Enqueue(e.Data);
            UnityEngine.Debug.Log(e.Data);
        };
    }

    ConcurrentQueue<string> incoming_messages = new ConcurrentQueue<string>();

    void Update()
    {
        if (incoming_messages.TryDequeue(out var message))
        {
            HandleMessage(message);
        }
    }

    private void HandleMessage(string message)
    {
        messageArray = message.Split(",");
        UnityEngine.Debug.Log("Color: " + messageArray[0] + ", Explosion: " + messageArray[1] + ", Sound: " + messageArray[2]);
       // BalloonSpawn(messageArray[0], messageArray[1]);
       // enable this is server is live
    }


    void BalloonSpawn() // add parameters string color and string explosion if live
    {
        // GameObject balloonPrefab = balloonPrefabs[UnityEngine.Random.Range(0, balloonPrefabs.Length)];
        int randomIndex = UnityEngine.Random.Range(0, colors.Count);
        int randomExpIndex = UnityEngine.Random.Range(0, explosions.Count);
       //  UnityEngine.Debug.Log(colors[randomIndex]);
        string color = colors[randomIndex]; // remove if live
        string explosion = explosions[randomExpIndex]; // remove if live

        spawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        UnityEngine.Debug.Log("Number of active balloons: " + (spawnedBalloons.Length + 1).ToString());
        if (spawnedBalloons.Length+1 <= maxNumBalloons)
        {
            Vector3 randomPos = GetARandomTreePos();
            GameObject balloon = null;
            if (explosion == "confetti")
            {
                balloon = Instantiate(confettiBalloonPrefab, new Vector3(randomPos[0], 0.4f, randomPos[2]), confettiBalloonPrefab.transform.rotation);
            }
            if (explosion == "water")
            {
                balloon = Instantiate(waterBalloonPrefab, new Vector3(randomPos[0], 0.4f, randomPos[2]), waterBalloonPrefab.transform.rotation);
            }
            if (explosion == "paint")
            {
                balloon = Instantiate(paintBalloonPrefab, new Vector3(randomPos[0], 0.4f, randomPos[2]), paintBalloonPrefab.transform.rotation);
            }
            if (explosion == "marbles")
            {
                balloon = Instantiate(marblesBalloonPrefab, new Vector3(randomPos[0], 0.4f, randomPos[2]), marblesBalloonPrefab.transform.rotation);
                //balloon = Instantiate(deathstarPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), discoBalloonPrefab.transform.rotation);
            }
            if (explosion == "disco")
            {
                balloon = Instantiate(discoBalloonPrefab, new Vector3(randomPos[0], 0.4f, randomPos[2]), discoBalloonPrefab.transform.rotation);
            }

            if (explosion == "deathstar")
            {
                balloon = Instantiate(deathstarBalloonPrefab, new Vector3(randomPos[0], 0.6f, randomPos[2]), deathstarBalloonPrefab.transform.rotation);
            }

            if (balloon != null)
            {
                SetBalloonColor(balloon, color);
                balloon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            
        }

        //if (balloonPrefab != null && spawnedBalloons.Length+1 <= maxNumBalloons)
        //{
        //    Vector3 randomPos = GetARandomTreePos();

        //    UnityEngine.Debug.Log("random pos: " + randomPos);
        //    GameObject balloon = Instantiate(balloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), balloonPrefab.transform.rotation);
        //    SetBalloonColor(balloon, color);

        //    balloon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //}
    }

    void SetBalloonColor(GameObject balloon, string color)
    {
        
        Renderer balloonRenderer = balloon.GetComponent<Renderer>();

        if (color == "red")
        {
            balloonRenderer.material = redBalloonMat;
        }
        if (color == "blue")
        {
            balloonRenderer.material = blueBalloonMat;
        }
        if (color == "green")
        {
            balloonRenderer.material = greenBalloonMat;
        }
        if (color == "pink")
        {
            balloonRenderer.material = pinkBalloonMat;
        }
    }

    public Vector3 GetARandomTreePos()
    {

        Mesh planeMesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(UnityEngine.Random.Range(minX, -minX),
                                     gameObject.transform.position.y,
                                     UnityEngine.Random.Range(minZ, -minZ));
        return newVec;
    }
}
