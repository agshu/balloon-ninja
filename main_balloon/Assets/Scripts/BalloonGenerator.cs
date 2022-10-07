using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    public GameObject[] balloonPrefabs;
    public float spawnTime = 2f;
    public int maxNumBalloons = 30;
    private GameObject[] spawnedBalloons;

    void Start()
    {
        InvokeRepeating("BalloonSpawn", spawnTime, spawnTime);
    }


    void BalloonSpawn()
    {
        GameObject balloonPrefab = balloonPrefabs[UnityEngine.Random.Range(0, balloonPrefabs.Length)]; 
        spawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        
        if (balloonPrefab != null && spawnedBalloons.Length+1 <= maxNumBalloons)
        {
            Vector3 randomPos = GetARandomTreePos();
            GameObject balloon = Instantiate(balloonPrefab, new Vector3(randomPos[0], 0.1f, randomPos[2]), balloonPrefab.transform.rotation);
            //GameObject balloon = Instantiate(balloonPrefab);
            balloon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
