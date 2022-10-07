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
    public GameObject[] balloonPrefabs;
    public float spawnTime = 3f;
    public int maxNumBalloons = 30;
    private GameObject[] spawnedBalloons;

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
            UnityEngine.Debug.Log("message " + message);
            HandleMessage(message);
        }
    }

    private void HandleMessage(string message)
    {
        UnityEngine.Debug.Log("Message from server " + message);

        messageArray = message.Split(";");
        UnityEngine.Debug.Log("Explosion: " + messageArray[0] + ", Color: " + messageArray[1] + ", Sound: " + messageArray[2]);
        if (messageArray[0] == "confetti")
        {
            BalloonSpawn(messageArray[1]);
        }


    }


    void BalloonSpawn(string color)
    {
        GameObject balloonPrefab = balloonPrefabs[UnityEngine.Random.Range(0, balloonPrefabs.Length)];
        spawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        
        if (balloonPrefab != null && spawnedBalloons.Length+1 <= maxNumBalloons)
        {
            Vector3 randomPos = GetARandomTreePos();
            GameObject balloon = Instantiate(balloonPrefab, randomPos, balloonPrefab.transform.rotation);
            SetBalloonColor(balloon, color);
            //GameObject balloon = Instantiate(balloonPrefab);
            balloon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
    }

    void SetBalloonColor(GameObject balloon, string color)
    {
        int[] rgbColor =  color.Split(',').Select(Int32.Parse).ToArray();

        Renderer balloonRenderer = balloon.GetComponent<Renderer>();

        balloonRenderer.material.color = new Color(rgbColor[0], rgbColor[1], rgbColor[2], 1);
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
