using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    public GameObject balloonPrefab;
    public float spawnTime = 3f;

    void Start()
    {
        InvokeRepeating("BalloonSpawn", spawnTime, spawnTime);
    }


    void BalloonSpawn()
    {
        if (balloonPrefab != null)
        {
            Vector3 randomPos = GetARandomTreePos();
            GameObject balloon = Instantiate(balloonPrefab, randomPos, Quaternion.identity);
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