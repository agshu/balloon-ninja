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

    // Balloon options
    public GameObject confettiBalloonPrefab;
    public GameObject waterBalloonPrefab;
    public GameObject paintBalloonPrefab;
    public GameObject marblesBalloonPrefab;

    // Material options
    public Material redBalloonMat;
    public Material blueBalloonMat;
    public Material greenBalloonMat;
    public Material pinkBalloonMat;

    private string[] messageArray;
    WebSocket ws;

    void Start()
    {
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
        UnityEngine.Debug.Log("Message from server " + message);

        messageArray = message.Split(",");
        UnityEngine.Debug.Log("Color: " + messageArray[0] + ", Explosion: " + messageArray[1] + ", Sound: " + messageArray[2]);
        BalloonSpawn(messageArray[0], messageArray[1]);
    }


    void BalloonSpawn(string color, string explosion)
    {
        // GameObject balloonPrefab = balloonPrefabs[UnityEngine.Random.Range(0, balloonPrefabs.Length)];

        spawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        UnityEngine.Debug.Log("Number of active balloons: " + (spawnedBalloons.Length + 1).ToString());
        if (spawnedBalloons.Length+1 <= maxNumBalloons)
        {
            Vector3 randomPos = GetARandomTreePos();
            UnityEngine.Debug.Log("Random pos on plane (aka rug): " + randomPos.ToString());
            GameObject balloon = null;

            if (explosion == "confetti")
            {
                balloon = Instantiate(confettiBalloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), confettiBalloonPrefab.transform.rotation);
            }
            if (explosion == "water")
            {
                balloon = Instantiate(waterBalloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), waterBalloonPrefab.transform.rotation);
            }
            if (explosion == "paint")
            {
                balloon = Instantiate(paintBalloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), paintBalloonPrefab.transform.rotation);
            }
            if (explosion == "marbles")
            {
                balloon = Instantiate(marblesBalloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), marblesBalloonPrefab.transform.rotation);
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
            // balloonRenderer.material.color = new Color(rgbColor[0], rgbColor[1], rgbColor[2], 1);
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
